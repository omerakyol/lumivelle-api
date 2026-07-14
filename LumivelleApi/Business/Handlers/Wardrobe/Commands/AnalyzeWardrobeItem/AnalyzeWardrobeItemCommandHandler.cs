using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Wardrobe.ValidationRules;
using Business.Services.Claude;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Wardrobe.Commands.AnalyzeWardrobeItem;

public class ClaudeWardrobeTagDto
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string[] Colors { get; set; }
    public string[] StyleTags { get; set; }
}

public class AnalyzeWardrobeItemCommandHandler(
    IBeautyProfileRepository beautyProfileRepository,
    IClaudeVisionService claudeVisionService,
    IHttpClientFactory httpClientFactory)
    : IRequestHandler<AnalyzeWardrobeItemCommandRequest, IDataResult<AnalyzeWardrobeItemResult>>
{
    private static readonly JsonSerializerOptions JsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    private const string SystemPrompt = """
        You are a fashion stylist cataloguing a single garment or accessory photo. Return a JSON object with these exact fields:

        {
          "name": string,        // short descriptive name, e.g. "Camel wool coat"
          "category": string,    // exactly one of: "Tops", "Bottoms", "Dresses", "Outerwear", "Shoes", "Accessories"
          "colors": string[],    // 1-3 dominant hex colors of the item
          "styleTags": string[]  // 2-5 lowercase style keywords, e.g. "minimalist", "old-money"
        }

        Return only valid JSON. No markdown, no explanation.
        """;

    [SecuredOperation(Priority = 1)]
    [ValidationAspect(typeof(AnalyzeWardrobeItemValidator), Priority = 2)]
    public async Task<IDataResult<AnalyzeWardrobeItemResult>> Handle(
        AnalyzeWardrobeItemCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var profile = await beautyProfileRepository.GetLatestByAccountIdAsync(accountId);
        var palette = profile?.Palette ?? [];

        var http = httpClientFactory.CreateClient();
        var imageBytes = await http.GetByteArrayAsync(request.ImageUrl, cancellationToken);
        var mediaType = request.ImageUrl.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
            ? "image/png"
            : "image/jpeg";

        var raw = await claudeVisionService.AnalyzeImageAsync(
            imageBytes, mediaType, SystemPrompt, "Catalogue this wardrobe item.", cancellationToken);

        var json = ExtractJson(raw);
        var parsed = JsonSerializer.Deserialize<ClaudeWardrobeTagDto>(json, JsonOptions)
                     ?? throw new ApplicationException("Claude returned unparseable wardrobe-tag JSON");

        var colors = parsed.Colors ?? [];
        var result = new AnalyzeWardrobeItemResult
        {
            Name = parsed.Name,
            Category = parsed.Category,
            Colors = colors,
            StyleTags = parsed.StyleTags ?? [],
            PaletteMatchScore = PaletteMatching.ScoreColorsAgainstPalette(colors, palette)
        };

        return new SuccessDataResult<AnalyzeWardrobeItemResult>(result);
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
