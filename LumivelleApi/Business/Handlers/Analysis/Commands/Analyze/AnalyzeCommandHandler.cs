using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Analysis.ValidationRules;
using Business.Services.Claude;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

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
    public string[] StyleReferences { get; set; }
    public string Headline { get; set; }
    public string Description { get; set; }
}

public class AnalyzeCommandHandler(
    IBeautyProfileRepository beautyProfileRepository,
    IClaudeVisionService claudeVisionService,
    IHttpClientFactory httpClientFactory)
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
          "styleReferences": string[], // 5 style aesthetic keywords
          "headline": string,          // one poetic line like "Warm Autumn, softly luminous"
          "description": string        // one sentence personalised description
        }

        Return only valid JSON. No markdown, no explanation.
        """;

    [SecuredOperation(Priority = 1)]
    [ValidationAspect(typeof(AnalyzeValidator), Priority = 2)]
    public async Task<IDataResult<BeautyProfileResult>> Handle(
        AnalyzeCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();

        var http = httpClientFactory.CreateClient();
        var imageBytes = await http.GetByteArrayAsync(request.ImageUrl, cancellationToken);
        var mediaType = request.ImageUrl.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
            ? "image/png"
            : "image/jpeg";

        var raw = await claudeVisionService.AnalyzeImageAsync(
            imageBytes, mediaType, SystemPrompt, "Analyze this selfie.", cancellationToken);

        var json = ExtractJson(raw);
        var parsed = JsonSerializer.Deserialize<ClaudeAnalysisDto>(json, JsonOptions)
                     ?? throw new ApplicationException("Claude returned unparseable analysis JSON");

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
            StyleReferences = parsed.StyleReferences ?? [],
            Headline = parsed.Headline,
            Description = parsed.Description,
            RawAnalysisJson = json
        };

        await beautyProfileRepository.AddAsync(document);

        return new SuccessDataResult<BeautyProfileResult>(BeautyProfileResult.FromDocument(document));
    }

    private static string ExtractJson(string raw)
    {
        var start = raw.IndexOf('{');
        var end = raw.LastIndexOf('}');
        if (start < 0 || end <= start)
            throw new ApplicationException("Claude response contained no JSON object");
        return raw.Substring(start, end - start + 1);
    }
}
