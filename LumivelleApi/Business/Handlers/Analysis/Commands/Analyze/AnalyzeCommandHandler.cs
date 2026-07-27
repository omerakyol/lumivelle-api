using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Analysis.ValidationRules;
using Business.Handlers.Media;
using Business.Services.Claude;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Business.Handlers.Analysis.Commands.Analyze;

public class ClaudeAnalysisDto
{
    public string Season { get; set; }
    public string Undertone { get; set; }
    public string Contrast { get; set; }
    public string FaceShape { get; set; }
    public string HairType { get; set; }
    public string[] Palette { get; set; }
    public string[] BestColors { get; set; }
    public string[] NeutralColors { get; set; }
    public string[] AvoidColors { get; set; }
    public MakeupBreakdown MakeupBreakdown { get; set; }
    public HairMetrics HairMetrics { get; set; }
    public string SkinType { get; set; }
    public string[] SkinConcerns { get; set; }
    public string SkinAnalysisNotes { get; set; }
    public string[] StyleReferences { get; set; }
    public string Headline { get; set; }
    public string Description { get; set; }
}

public class AnalyzeCommandHandler(
    IBeautyProfileRepository beautyProfileRepository,
    IClaudeVisionService claudeVisionService,
    IAccountRepository accountRepository,
    IHttpContextAccessor httpContextAccessor,
    IWebHostEnvironment webHostEnvironment)
    : IRequestHandler<AnalyzeCommandRequest, IDataResult<BeautyProfileResult>>
{
    private static readonly JsonSerializerOptions JsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    private const string SystemPrompt = """
        You are a professional colour analyst and beauty consultant. Analyze the provided selfie photograph and return a JSON object with these exact fields:

        {
          "season": string,         // one of: "Spring", "Light Spring", "Warm Spring", "Clear Spring", "Summer", "Light Summer", "Cool Summer", "Soft Summer", "Autumn", "Soft Autumn", "Warm Autumn", "Deep Autumn", "Winter", "Deep Winter", "Cool Winter", "Clear Winter"
          "undertone": string,      // one of: "Warm", "Cool", "Neutral"
          "contrast": string,       // one of: "Low", "Medium", "High"
          "faceShape": string,      // one of: "Oval", "Round", "Square", "Heart", "Oblong", "Diamond"
          "hairType": string,       // one of: "Straight", "Wavy", "Curly", "Coily"
          "palette": string[],      // 10 hex colors from the seasonal palette (best colors to wear)
          "bestColors": string[],   // same 10 as palette (kept for schema symmetry)
          "neutralColors": string[], // 5 neutral hex colors
          "avoidColors": string[],  // 5 hex colors to avoid
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
          "styleReferences": string[], // 5 style aesthetic keywords
          "headline": string,          // one poetic line like "Warm Autumn, softly luminous"
          "description": string        // one sentence personalised description
        }

        Return only valid JSON. No markdown, no explanation.
        """;

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

        var mediaType = GetMediaType(request.File.FileName);

        var raw = await claudeVisionService.AnalyzeImageAsync(
            imageBytes, mediaType, SystemPrompt, "Analyze this selfie.", cancellationToken);

        var json = ExtractJson(raw);
        var parsed = JsonSerializer.Deserialize<ClaudeAnalysisDto>(json, JsonOptions)
                     ?? throw new ApplicationException("Claude returned unparseable analysis JSON");

        var mediaFolder = Path.Combine(webHostEnvironment.WebRootPath, "media");
        var saved = await MediaStorage.SaveFileAsync(
            request.File, mediaFolder, httpContextAccessor.HttpContext?.Request, cancellationToken);

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
            StyleReferences = parsed.StyleReferences ?? [],
            Headline = parsed.Headline,
            Description = parsed.Description,
            RawAnalysisJson = json,
            PhotoUrl = saved.FileUrl
        };

        await beautyProfileRepository.AddAsync(document);

        return new SuccessDataResult<BeautyProfileResult>(BeautyProfileResult.FromDocument(document));
    }

    private static string GetMediaType(string fileName) =>
        Path.GetExtension(fileName).ToLowerInvariant() switch
        {
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".webp" => "image/webp",
            ".heic" => "image/heic",
            ".heif" => "image/heif",
            ".avif" => "image/avif",
            _ => "image/jpeg"
        };

    private static string ExtractJson(string raw)
    {
        var start = raw.IndexOf('{');
        var end = raw.LastIndexOf('}');
        if (start < 0 || end <= start)
            throw new ApplicationException("Claude response contained no JSON object");
        return raw.Substring(start, end - start + 1);
    }
}
