using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    IMakeupLookRepository makeupLookRepository,
    IHairstyleRepository hairstyleRepository,
    IStyleDnaRepository styleDnaRepository,
    IColorPaletteRepository colorPaletteRepository,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<AnalyzeCommandRequest, IDataResult<BeautyProfileResult>>
{
    private const string SystemPrompt = """
                                        You are a professional colour analyst. Analyze the provided selfie photograph and return a JSON object with these exact fields:

                                        {
                                          "season": string,         // one of: "Spring", "Light Spring", "Warm Spring", "Clear Spring", "Summer", "Light Summer", "Cool Summer", "Soft Summer", "Autumn", "Soft Autumn", "Warm Autumn", "Deep Autumn", "Winter", "Deep Winter", "Cool Winter", "Clear Winter"
                                          "undertone": string,      // one of: "Warm", "Cool", "Neutral"
                                          "contrast": string,       // one of: "Low", "Medium", "High"
                                          "faceShape": string,      // one of: "Oval", "Round", "Square", "Heart", "Oblong", "Diamond"
                                          "hairType": string,       // one of: "Straight", "Wavy", "Curly", "Coily"
                                          "eyeColor": string,       // one of: "Brown", "Hazel", "Green", "Blue", "Gray"
                                          "eyeShape": string,       // one of: "Almond", "Round", "Hooded", "Monolid", "Downturned", "Upturned"
                                          "hairMetrics": {
                                            "faceShapeDetail": string,
                                            "jawline": string,     // one of: "Soft", "Medium", "Angular"
                                            "forehead": string,    // one of: "Small", "Medium", "Large"
                                            "density": string      // one of: "Low", "Medium", "High"
                                          },
                                          "skinType": string,        // one of: "Oily", "Dry", "Combination", "Normal", "Sensitive"
                                          "skinConcerns": string[],  // visible skin concerns, e.g. "Acne", "Wrinkles", "Redness", "Hyperpigmentation", "Dark Circles", "Enlarged Pores", "Dryness", "Dullness". Empty array if none are visible.
                                          "skinAnalysisNotes": string, // one short sentence summarising the skin concerns, for later product recommendations
                                          "skinTone": ColorSwatch,     // the detected natural skin tone itself, for foundation/concealer shade matching
                                          "metalTone": string         // one of: "Gold", "Silver", "Rose Gold", "Neutral" — which jewelry/accessory metal suits them
                                        }

                                        Where ColorSwatch is:
                                        {
                                          "name": string,  // generic color name, e.g. "Beige"
                                          "code": string,  // stylist/marketing code name, e.g. "Neutral Beige"
                                          "hex": string    // hex value, e.g. "#E8DCC8"
                                        }

                                        Do not recommend products, makeup, hairstyles, or styles. Only classify. Return only valid JSON. No markdown, no explanation.
                                        """;

    private static readonly JsonSerializerOptions JsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    // {0} = undertone (lowercase), {1} = season, {2} = top matched Style DNA name
    private static readonly Dictionary<string, string> DescriptionWithStyleDnaTemplates = new()
    {
        ["en"] = "Your {0}-toned {1} palette pairs beautifully with {2}.",
        ["tr"] = "{0} tonlu {1} paletiniz {2} ile mükemmel uyum sağlıyor.",
        ["fr"] = "Votre palette {1} aux tons {0} s'accorde à merveille avec {2}.",
        ["es"] = "Tu paleta {1} de tono {0} combina maravillosamente con {2}.",
        ["ar"] = "تنسجم لوحة ألوانك {1} ذات الدرجة {0} بشكل رائع مع {2}.",
        ["ru"] = "Ваша палитра {1} с {0} подтоном прекрасно сочетается с {2}."
    };

    // {0} = undertone (lowercase), {1} = season
    private static readonly Dictionary<string, string> DescriptionFallbackTemplates = new()
    {
        ["en"] = "Your {0}-toned {1} palette, uniquely yours.",
        ["tr"] = "{0} tonlu {1} paletiniz, size özel.",
        ["fr"] = "Votre palette {1} aux tons {0}, unique en son genre.",
        ["es"] = "Tu paleta {1} de tono {0}, únicamente tuya.",
        ["ar"] = "لوحة ألوانك {1} ذات الدرجة {0}، فريدة بك وحدك.",
        ["ru"] = "Ваша палитра {1} с {0} подтоном — только ваша."
    };

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

        if (account.SubscriptionTier == SubscriptionTier.Free)
        {
            var recentCount = await beautyProfileRepository.CountAsync(x =>
                x.AccountId == accountId && x.CreatedAt >= DateTime.UtcNow.AddDays(-30));
            if (recentCount >= 1)
                throw new ApplicationException(Messages.AnalysisLimitReached);
        }

        await using var memoryStream = new MemoryStream();
        await request.File.CopyToAsync(memoryStream, cancellationToken);
        var imageBytes = memoryStream.ToArray();

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
            EyeColor = parsed.EyeColor,
            EyeShape = parsed.EyeShape,
            HairMetrics = parsed.HairMetrics,
            SkinType = parsed.SkinType,
            SkinConcerns = parsed.SkinConcerns ?? [],
            SkinAnalysisNotes = parsed.SkinAnalysisNotes,
            SkinTone = parsed.SkinTone,
            MetalTone = parsed.MetalTone,
            RawAnalysisJson = json,
            PhotoUrl = photoUrl
        };

        var palette = await colorPaletteRepository.GetBySeasonAsync(document.Season)
                      ?? throw new ApplicationException($"No ColorPaletteDocument seeded for season '{document.Season}'");
        document.Palette = palette.BestColors;
        document.BestColors = palette.BestColors;
        document.NeutralColors = palette.NeutralColors;
        document.AvoidColors = palette.AvoidColors;

        var accountLanguage = string.IsNullOrWhiteSpace(account.Language) ? "en" : account.Language;

        var allLooks = await makeupLookRepository.GetAllAsync();
        var scoredLooks = allLooks
            .Select(l => (Look: l, Tier: RecommendationEngine.ScoreMakeupLook(l, document)))
            .ToList();
        document.BestMakeupLooks = scoredLooks.Where(x => x.Tier == RecommendationTier.Best)
            .Take(3).Select(x => ToTieredMakeupLook(x.Look, accountLanguage)).ToArray();
        document.GoodMakeupLooks = scoredLooks.Where(x => x.Tier == RecommendationTier.Good)
            .Take(2).Select(x => ToTieredMakeupLook(x.Look, accountLanguage)).ToArray();
        document.AvoidMakeupLooks = scoredLooks.Where(x => x.Tier == RecommendationTier.Avoid)
            .Take(2).Select(x => ToTieredMakeupLook(x.Look, accountLanguage)).ToArray();

        var allHairstyles = await hairstyleRepository.GetAllAsync();
        var scoredHairstyles = allHairstyles
            .Select(h => (Hairstyle: h, Tier: RecommendationEngine.ScoreHairstyle(h, document)))
            .ToList();
        document.BestHairstyles = scoredHairstyles.Where(x => x.Tier == RecommendationTier.Best)
            .Take(5).Select(x => ToTieredHairstyle(x.Hairstyle, accountLanguage)).ToArray();
        document.GoodHairstyles = scoredHairstyles.Where(x => x.Tier == RecommendationTier.Good)
            .Take(3).Select(x => ToTieredHairstyle(x.Hairstyle, accountLanguage)).ToArray();

        var allStyleDnas = await styleDnaRepository.GetAllAsync();
        var scoredStyleDnas = allStyleDnas
            .Select(s => (StyleDna: s, Tier: RecommendationEngine.ScoreStyleDna(s, document)))
            .ToList();
        document.BestStyleDnas = scoredStyleDnas.Where(x => x.Tier == RecommendationTier.Best)
            .Take(4).Select(x => ToTieredStyleDna(x.StyleDna, accountLanguage)).ToArray();

        var topStyleDna = document.BestStyleDnas.FirstOrDefault();
        var undertoneLower = (document.Undertone ?? string.Empty).ToLowerInvariant();
        document.Headline = topStyleDna != null
            ? $"{document.Season}, {topStyleDna.Name}"
            : document.Season;
        document.Description = topStyleDna != null
            ? string.Format(Localize(DescriptionWithStyleDnaTemplates, accountLanguage), undertoneLower, document.Season, topStyleDna.Name)
            : string.Format(Localize(DescriptionFallbackTemplates, accountLanguage), undertoneLower, document.Season);

        await beautyProfileRepository.AddAsync(document);

        return new SuccessDataResult<BeautyProfileResult>(BeautyProfileResult.FromDocument(document));
    }

    private static TieredMakeupLook ToTieredMakeupLook(MakeupLookDocument look, string language) => new()
    {
        Id = look.Id.ToString(),
        Title = Localize(look.Title, language),
        Lips = look.Lips,
        Cheeks = look.Cheeks,
        Contour = look.Contour,
        Eyeshadow = look.Eyeshadow,
        Liner = look.Liner,
        Brow = look.Brow
    };

    private static TieredHairstyle ToTieredHairstyle(HairstyleDocument hairstyle, string language) => new()
    {
        Id = hairstyle.Id.ToString(),
        Title = Localize(hairstyle.Title, language),
        Description = Localize(hairstyle.Description, language)
    };

    private static TieredStyleDna ToTieredStyleDna(StyleDnaDocument styleDna, string language) => new()
    {
        Id = styleDna.Id.ToString(),
        Name = Localize(styleDna.Name, language),
        SignaturePieces = styleDna.SignaturePieces.TryGetValue(language, out var pieces)
            ? pieces : styleDna.SignaturePieces.GetValueOrDefault("en", []),
        Keywords = styleDna.Keywords.TryGetValue(language, out var keywords)
            ? keywords : styleDna.Keywords.GetValueOrDefault("en", [])
    };

    private const string DefaultLanguage = "en";

    private static string Localize(Dictionary<string, string> values, string language)
    {
        if (values == null || values.Count == 0) return string.Empty;
        if (values.TryGetValue(language, out var localized) && !string.IsNullOrEmpty(localized))
            return localized;
        if (values.TryGetValue(DefaultLanguage, out var fallback) && !string.IsNullOrEmpty(fallback))
            return fallback;
        return values.Values.First();
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
