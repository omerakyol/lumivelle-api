using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Wardrobe.ValidationRules;
using Business.Services.AiServices;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Wardrobe.Commands.AnalyzeWardrobeItem;

public class WardrobeTagAiDto
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string[] Colors { get; set; }
    public string[] StyleTags { get; set; }
}

public class AnalyzeWardrobeItemCommandHandler(
    IBeautyProfileRepository beautyProfileRepository,
    IAiServiceFactory aiServiceFactory,
    IHttpClientFactory httpClientFactory,
    IAccountRepository accountRepository)
    : IRequestHandler<AnalyzeWardrobeItemCommandRequest, IDataResult<AnalyzeWardrobeItemResult>>
{
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

    private static readonly JsonSerializerOptions JsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    [ValidationAspect(typeof(AnalyzeWardrobeItemValidator), Priority = 2)]
    public async Task<IDataResult<AnalyzeWardrobeItemResult>> Handle(
        AnalyzeWardrobeItemCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var profile = await beautyProfileRepository.GetLatestByAccountIdAsync(accountId);
        var palette = profile?.Palette?.Select(c => c.Hex).ToArray() ?? [];

        var http = httpClientFactory.CreateClient();
        var imageBytes = await http.GetByteArrayAsync(request.ImageUrl, cancellationToken);
        var mediaType = request.ImageUrl.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
            ? "image/png"
            : "image/jpeg";

        var aiService = aiServiceFactory.Get(AiProvider.OpenAi);
        var raw = await aiService.AnalyzeImageAsync(
            imageBytes, mediaType, SystemPrompt, "Catalogue this wardrobe item.", cancellationToken);

        var json = ExtractJson(raw);
        var parsed = JsonSerializer.Deserialize<WardrobeTagAiDto>(json, JsonOptions)
                     ?? throw new ApplicationException("AiServices returned unparseable wardrobe-tag JSON");

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
            throw new ApplicationException("AiServices response contained no JSON object");
        return raw.Substring(start, end - start + 1);
    }
}