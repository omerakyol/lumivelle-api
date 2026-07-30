using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Analysis.ValidationRules;
using Business.Handlers.Media;
using Business.Services.AiServices;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Ai;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Analysis.Commands.Analyze;

public class AnalyzeCommandHandler(
    IBeautyProfileRepository beautyProfileRepository,
    IAiServiceFactory aiServiceFactory,
    IAccountRepository accountRepository,
    IMediaFileRepository mediaFileRepository,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<AnalyzeCommandRequest, IDataResult<BeautyProfileResult>>
{
    private const string SystemPrompt = """
                                        You are a professional colour analyst and beauty consultant. Analyze the provided selfie photograph and return a JSON object with these exact fields:

                                        {
                                          "season": string,         // one of: "Spring", "Light Spring", "Warm Spring", "Clear Spring", "Summer", "Light Summer", "Cool Summer", "Soft Summer", "Autumn", "Soft Autumn", "Warm Autumn", "Deep Autumn", "Winter", "Deep Winter", "Cool Winter", "Clear Winter"
                                          "undertone": string,      // one of: "Warm", "Cool", "Neutral"
                                          "contrast": string,       // one of: "Low", "Medium", "High"
                                          "faceShape": string,      // one of: "Oval", "Round", "Square", "Heart", "Oblong", "Diamond"
                                          "hairType": string,       // one of: "Straight", "Wavy", "Curly", "Coily"
                                          "palette": ColorSwatch[],      // 10 colors from the seasonal palette (best colors to wear)
                                          "bestColors": ColorSwatch[],   // same 10 as palette (kept for schema symmetry)
                                          "neutralColors": ColorSwatch[], // 5 neutral colors
                                          "avoidColors": ColorSwatch[],  // 5 colors to avoid
                                          "makeupBreakdown": {
                                            "lips": string,         // shade description
                                            "lipsHex": string,
                                            "cheeks": string,
                                            "cheeksHex": string,
                                            "contour": string,
                                            "contourHex": string,
                                            "eyeshadow": string,
                                            "eyeshadowHex": string,
                                            "liner": string,
                                            "linerHex": string,
                                            "brow": string,
                                            "browHex": string
                                          },
                                          "hairMetrics": {
                                            "faceShapeDetail": string,
                                            "jawline": string,
                                            "forehead": string,
                                            "density": string
                                          },
                                          "skinType": string,        // one of: "Oily", "Dry", "Combination", "Normal", "Sensitive"
                                          "skinConcerns": string[],  // visible skin concerns, e.g. "Acne", "Wrinkles", "Redness", "Hyperpigmentation", "Dark Circles", "Enlarged Pores", "Dryness", "Dullness". Empty array if none are visible.
                                          "skinAnalysisNotes": string, // one short sentence summarising the skin concerns, for later product recommendations
                                          "skinTone": ColorSwatch,     // the detected natural skin tone itself, for foundation/concealer shade matching
                                          "metalTone": string,         // one of: "Gold", "Silver", "Rose Gold", "Neutral" — which jewelry/accessory metal suits them, for jewelry recommendations
                                          "recommendedProductCategories": string[], // 3-6 product categories worth recommending, e.g. "Foundation", "Blush", "Lipstick", "Eyeshadow Palette", "Hair Color"
                                          "styleReferences": string[], // 5 style aesthetic keywords
                                          "headline": string,          // one poetic line like "Warm Autumn, softly luminous"
                                          "description": string        // one sentence personalised description
                                        }

                                        Where ColorSwatch is:
                                        {
                                          "name": string,  // generic color name, e.g. "Beige"
                                          "code": string,  // stylist/marketing code name, e.g. "Neutral Beige"
                                          "hex": string    // hex value, e.g. "#E8DCC8"
                                        }

                                        Return only valid JSON. No markdown, no explanation.
                                        """;

    private static readonly JsonSerializerOptions JsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    [ValidationAspect(typeof(AnalyzeValidator), Priority = 1)]
    public async Task<IDataResult<BeautyProfileResult>> Handle(
        AnalyzeCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        await using var memoryStream = new MemoryStream();
        await request.File.CopyToAsync(memoryStream, cancellationToken);
        var imageBytes = memoryStream.ToArray();

        // Compress once and reuse the same small JPEG both for the AI call and for storage,
        // instead of paying the resize/encode cost twice on the original (possibly multi-MB) upload.
        var (compressedBytes, compressedMediaType) =
            await AiImageCompressor.CompressAsync(imageBytes, cancellationToken);

        var aiVisionService = aiServiceFactory.Get(AiProvider.OpenAi);

        var raw = await aiVisionService.AnalyzeImageAsync(
            compressedBytes, compressedMediaType, SystemPrompt, "Analyze this selfie.", cancellationToken);

        var json = ExtractJson(raw);
        var parsed = JsonSerializer.Deserialize<AiAnalysisDto>(json, JsonOptions)
                     ?? throw new ApplicationException("AiServices returned unparseable analysis JSON");

        var photoFileId = await mediaFileRepository.UploadAsync(
            compressedBytes,
            $"beauty-{accountId}-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.jpg",
            compressedMediaType,
            accountId,
            cancellationToken);
        var photoUrl =
            $"{MediaStorage.BuildBaseUrl(httpContextAccessor.HttpContext?.Request)}/api/media/gridfs/{photoFileId}";

        var document = new BeautyProfileDocument
        {
            AccountId = accountId,
            Season = parsed.Season,
            Undertone = parsed.Undertone,
            Contrast = parsed.Contrast,
            FaceShape = parsed.FaceShape,
            HairType = parsed.HairType,
            Palette = parsed.Palette ?? [],
            BestColors = parsed.BestColors ?? [],
            NeutralColors = parsed.NeutralColors ?? [],
            AvoidColors = parsed.AvoidColors ?? [],
            MakeupBreakdown = parsed.MakeupBreakdown,
            HairMetrics = parsed.HairMetrics,
            SkinType = parsed.SkinType,
            SkinConcerns = parsed.SkinConcerns ?? [],
            SkinAnalysisNotes = parsed.SkinAnalysisNotes,
            SkinTone = parsed.SkinTone,
            MetalTone = parsed.MetalTone,
            RecommendedProductCategories = parsed.RecommendedProductCategories ?? [],
            StyleReferences = parsed.StyleReferences ?? [],
            Headline = parsed.Headline,
            Description = parsed.Description,
            RawAnalysisJson = json,
            PhotoUrl = photoUrl
        };

        await beautyProfileRepository.AddAsync(document);

        return new SuccessDataResult<BeautyProfileResult>(BeautyProfileResult.FromDocument(document));
    }

    private static string ExtractJson(string raw)
    {
        var start = raw.IndexOf('{');
        var end = raw.LastIndexOf('}');
        if (start < 0 || end <= start)
            throw new ApplicationException("AiServices response contained no JSON object");
        return raw.Substring(start, end - start + 1);
    }
}