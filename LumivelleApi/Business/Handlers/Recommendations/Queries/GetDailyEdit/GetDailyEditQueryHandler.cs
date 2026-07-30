using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Recommendations.ValidationRules;
using Business.Handlers.Wardrobe;
using Business.Services.AiServices;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Recommendations.Queries.GetDailyEdit;

public class ClaudeDailyEditDto
{
    public string DailyEditTitle { get; set; }
    public string DailyEditSubtitle { get; set; }
    public string[] MakeupRecTitles { get; set; }
}

public class GetDailyEditQueryHandler(
    IBeautyProfileRepository beautyProfileRepository,
    IWardrobeItemRepository wardrobeItemRepository,
    IDailyRecommendationRepository dailyRecommendationRepository,
    IAiServiceFactory aiServiceFactory,
    IAccountRepository accountRepository)
    : IRequestHandler<GetDailyEditQueryRequest, IDataResult<DailyEditResult>>
{
    private const string SystemPrompt = """
                                        You are a beauty editor writing one day's personalised recommendation copy for a user. Return a JSON object with these exact fields:

                                        {
                                          "dailyEditTitle": string,      // short poetic title, e.g. "Golden hour glow"
                                          "dailyEditSubtitle": string,   // one descriptive sentence
                                          "makeupRecTitles": string[]    // exactly 3 short makeup look titles
                                        }

                                        Return only valid JSON. No markdown, no explanation.
                                        """;

    private static readonly JsonSerializerOptions JsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    [ValidationAspect(typeof(GetDailyEditValidator), Priority = 2)]
    public async Task<IDataResult<DailyEditResult>> Handle(
        GetDailyEditQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var cached = await dailyRecommendationRepository.GetByAccountAndDateAsync(accountId, request.LocalDate);
        if (cached != null)
            return new SuccessDataResult<DailyEditResult>(MapToResult(cached));

        var profile = await beautyProfileRepository.GetLatestByAccountIdAsync(accountId);
        if (profile == null)
            throw new ApplicationException(Messages.BeautyProfileNotFound);

        var items = await wardrobeItemRepository.GetByAccountIdAsync(accountId, null);
        var paletteHex = profile.Palette?.Select(c => c.Hex).ToArray() ?? [];
        var outfitRec = BuildOutfitRec(items, paletteHex);

        ClaudeDailyEditDto copy;
        var shouldCache = true;
        try
        {
            var userPrompt = $"Season: {profile.Season}. Palette: {string.Join(", ", paletteHex)}.";
            var aiService = aiServiceFactory.Get(AiProvider.OpenAi);
            var raw = await aiService.AnalyzeTextAsync(SystemPrompt, userPrompt, cancellationToken);
            var json = ExtractJson(raw);
            copy = JsonSerializer.Deserialize<ClaudeDailyEditDto>(json, JsonOptions)
                   ?? throw new ApplicationException("AiServices returned unparseable daily-edit JSON");
        }
        catch (Exception)
        {
            copy = BuildSeededCopy(GetSeasonFamily(profile.Season));
            shouldCache = false;
        }

        var result = new DailyEditResult
        {
            Season = profile.Season,
            Palette = paletteHex,
            DailyEdit = new DailyEditItem
            {
                Title = copy.DailyEditTitle,
                Subtitle = copy.DailyEditSubtitle,
                ImageUrl = $"https://picsum.photos/seed/{request.LocalDate}-edit/800/1000"
            },
            MakeupRecs = (copy.MakeupRecTitles ?? [])
                .Select((title, i) => new MakeupRecItem
                {
                    Title = title,
                    ImageUrl = $"https://picsum.photos/seed/{request.LocalDate}-mk{i}/400/400"
                })
                .ToList(),
            OutfitRec = outfitRec,
            Trending = BuildTrending(GetSeasonFamily(profile.Season))
        };

        if (shouldCache)
        {
            var document = new DailyRecommendationDocument
            {
                AccountId = accountId,
                LocalDate = request.LocalDate,
                Season = result.Season,
                Palette = result.Palette,
                DailyEdit = new DailyEditItemValue
                {
                    Title = result.DailyEdit.Title,
                    Subtitle = result.DailyEdit.Subtitle,
                    ImageUrl = result.DailyEdit.ImageUrl
                },
                MakeupRecs = result.MakeupRecs
                    .Select(m => new MakeupRecItemValue { Title = m.Title, ImageUrl = m.ImageUrl })
                    .ToArray(),
                OutfitRec = new OutfitRecItemValue
                {
                    Title = result.OutfitRec.Title,
                    Description = result.OutfitRec.Description,
                    MatchScore = result.OutfitRec.MatchScore,
                    ImageUrl = result.OutfitRec.ImageUrl,
                    WardrobeItemId = string.IsNullOrEmpty(result.OutfitRec.WardrobeItemId)
                        ? null
                        : ObjectId.Parse(result.OutfitRec.WardrobeItemId)
                },
                Trending = result.Trending
                    .Select(t => new TrendingItemValue { Title = t.Title, ImageUrl = t.ImageUrl })
                    .ToArray()
            };

            await dailyRecommendationRepository.AddAsync(document);
        }

        return new SuccessDataResult<DailyEditResult>(result);
    }

    private static OutfitRecItem BuildOutfitRec(List<WardrobeItemDocument> items, string[] palette)
    {
        if (items.Count == 0)
        {
            var seeded = BuildSeededCopy(GetSeasonFamily(null));
            return new OutfitRecItem
            {
                Title = seeded.MakeupRecTitles?.FirstOrDefault() ?? "Add your wardrobe",
                Description = "Built around your seasonal palette and saved style preferences.",
                MatchScore = 0,
                ImageUrl = "https://picsum.photos/seed/no-wardrobe/400/500",
                WardrobeItemId = null
            };
        }

        var scored = items
            .Select(item => (
                Item: item,
                Score: PaletteMatching.ScoreColorsAgainstPalette(item.Colors, palette)
                       + (item.IsFavorite ? 5 : 0)
                       + (item.LastWornAt == null || item.LastWornAt < DateTime.UtcNow.AddDays(-14) ? 5 : 0)))
            .OrderByDescending(x => x.Score)
            .ToList();

        var anchor = scored.First();

        return new OutfitRecItem
        {
            Title = anchor.Item.Name,
            Description = "Built around your seasonal palette and saved style preferences.",
            MatchScore = Math.Min(anchor.Score, 100),
            ImageUrl = anchor.Item.ImageUrl,
            WardrobeItemId = anchor.Item.Id.ToString()
        };
    }

    private static List<TrendingItem> BuildTrending(string family)
    {
        var seed = family.ToLowerInvariant();
        return
        [
            new TrendingItem { Title = "Quiet luxury", ImageUrl = $"https://picsum.photos/seed/{seed}-tr1/400/540" },
            new TrendingItem { Title = "Soft glam", ImageUrl = $"https://picsum.photos/seed/{seed}-tr2/400/540" },
            new TrendingItem { Title = "Minimal lines", ImageUrl = $"https://picsum.photos/seed/{seed}-tr3/400/540" },
            new TrendingItem { Title = "Warm neutrals", ImageUrl = $"https://picsum.photos/seed/{seed}-tr4/400/540" }
        ];
    }

    private static string GetSeasonFamily(string season)
    {
        if (season == null) return "Autumn";
        if (season.Contains("Spring")) return "Spring";
        if (season.Contains("Summer")) return "Summer";
        if (season.Contains("Winter")) return "Winter";
        return "Autumn";
    }

    private static ClaudeDailyEditDto BuildSeededCopy(string family)
    {
        var titles = new Dictionary<string, (string Edit, string Sub)>
        {
            ["Spring"] = ("Fresh morning bloom", "Light, luminous tones for your warm brightness"),
            ["Summer"] = ("Cool breeze elegance", "Muted rose and dusty blue for your soft coolness"),
            ["Autumn"] = ("Golden hour glow", "Warm terracotta and olive for your rich depth"),
            ["Winter"] = ("Crisp contrast", "Jewel tones and true black for your clarity")
        };
        var t = titles[family];

        return new ClaudeDailyEditDto
        {
            DailyEditTitle = t.Edit,
            DailyEditSubtitle = t.Sub,
            MakeupRecTitles = ["Everyday radiance", "Soft evening", "Weekend glow"]
        };
    }

    private static DailyEditResult MapToResult(DailyRecommendationDocument document)
    {
        return new DailyEditResult
        {
            Season = document.Season,
            Palette = document.Palette,
            DailyEdit = new DailyEditItem
            {
                Title = document.DailyEdit.Title,
                Subtitle = document.DailyEdit.Subtitle,
                ImageUrl = document.DailyEdit.ImageUrl
            },
            MakeupRecs = document.MakeupRecs
                .Select(m => new MakeupRecItem { Title = m.Title, ImageUrl = m.ImageUrl })
                .ToList(),
            OutfitRec = new OutfitRecItem
            {
                Title = document.OutfitRec.Title,
                Description = document.OutfitRec.Description,
                MatchScore = document.OutfitRec.MatchScore,
                ImageUrl = document.OutfitRec.ImageUrl,
                WardrobeItemId = document.OutfitRec.WardrobeItemId?.ToString()
            },
            Trending = document.Trending
                .Select(t => new TrendingItem { Title = t.Title, ImageUrl = t.ImageUrl })
                .ToList()
        };
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