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

public class ClaudeMakeupRecDto
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Icon { get; set; }
}

public class ClaudeDailyEditDto
{
    public string DailyEditTitle { get; set; }
    public string DailyEditSubtitle { get; set; }
    public string Description { get; set; }
    public ClaudeMakeupRecDto[] MakeupRecs { get; set; }
    public string[] AccessoryTitles { get; set; }
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
                                          "dailyEditSubtitle": string,   // one short descriptive sentence
                                          "description": string,         // 1-2 sentence paragraph, second person, explaining today's recommendation
                                          "makeupRecs": [                // exactly 3 items
                                            { "title": string, "subtitle": string, "icon": string }
                                          ],
                                          "accessoryTitles": string[]    // exactly 3 short accessory names, e.g. "Gold hoops"
                                        }

                                        "icon" must be one of: sparkles, sun, flower, star, droplet.

                                        Return only valid JSON. No markdown, no explanation.
                                        """;

    private static readonly JsonSerializerOptions JsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    private static readonly HashSet<string> AllowedIcons =
        new(StringComparer.OrdinalIgnoreCase) { "sparkles", "sun", "flower", "star", "droplet" };

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
                Description = copy.Description,
                ImageUrl = $"https://picsum.photos/seed/{request.LocalDate}-edit/800/1000"
            },
            MakeupRecs = (copy.MakeupRecs ?? [])
                .Select((m, i) => new MakeupRecItem
                {
                    Title = m.Title,
                    Subtitle = m.Subtitle,
                    Icon = SanitizeIcon(m.Icon),
                    ImageUrl = $"https://picsum.photos/seed/{request.LocalDate}-mk{i}/400/400"
                })
                .ToList(),
            OutfitRec = outfitRec,
            Trending = BuildTrending(GetSeasonFamily(profile.Season)),
            Colors = (profile.Palette ?? [])
                .Take(5)
                .Select(c => new ColorItem { Name = c.Name, Hex = c.Hex })
                .ToList(),
            MakeupDetails = BuildMakeupDetails(profile.MakeupBreakdown),
            Accessories = (copy.AccessoryTitles ?? [])
                .Select((title, i) => new AccessoryItem
                {
                    Title = title,
                    ImageUrl = $"https://picsum.photos/seed/{request.LocalDate}-acc{i}/400/400"
                })
                .ToList()
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
                    Description = result.DailyEdit.Description,
                    ImageUrl = result.DailyEdit.ImageUrl
                },
                MakeupRecs = result.MakeupRecs
                    .Select(m => new MakeupRecItemValue
                        { Title = m.Title, Subtitle = m.Subtitle, Icon = m.Icon, ImageUrl = m.ImageUrl })
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
                    .ToArray(),
                Colors = result.Colors
                    .Select(c => new ColorItemValue { Name = c.Name, Hex = c.Hex })
                    .ToArray(),
                MakeupDetails = result.MakeupDetails
                    .Select(m => new MakeupDetailItemValue { Type = m.Type, Value = m.Value, Icon = m.Icon })
                    .ToArray(),
                Accessories = result.Accessories
                    .Select(a => new AccessoryItemValue { Title = a.Title, ImageUrl = a.ImageUrl })
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
                Title = seeded.MakeupRecs?.FirstOrDefault()?.Title ?? "Add your wardrobe",
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

    private static string SanitizeIcon(string icon) =>
        AllowedIcons.Contains(icon ?? "") ? icon.ToLowerInvariant() : "sparkles";

    private static List<MakeupDetailItem> BuildMakeupDetails(MakeupBreakdown breakdown)
    {
        if (breakdown == null) return [];
        var items = new List<MakeupDetailItem>();
        if (!string.IsNullOrWhiteSpace(breakdown.Lips))
            items.Add(new MakeupDetailItem { Type = "Lip", Value = breakdown.Lips, Icon = "sparkles" });
        if (!string.IsNullOrWhiteSpace(breakdown.Cheeks))
            items.Add(new MakeupDetailItem { Type = "Cheek", Value = breakdown.Cheeks, Icon = "flower" });
        if (!string.IsNullOrWhiteSpace(breakdown.Eyeshadow))
            items.Add(new MakeupDetailItem { Type = "Eye", Value = breakdown.Eyeshadow, Icon = "eye" });
        return items;
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
        var titles = new Dictionary<string, (string Edit, string Sub, string Desc)>
        {
            ["Spring"] = ("Fresh morning bloom", "Light, luminous tones for your warm brightness",
                "Today's light favors clear, fresh tones — Lumi leaned into bright coral and soft green to keep your glow luminous."),
            ["Summer"] = ("Cool breeze elegance", "Muted rose and dusty blue for your soft coolness",
                "Today's light is soft and cool — Lumi leaned into dusty rose and slate blue to keep your look effortless."),
            ["Autumn"] = ("Golden hour glow", "Warm terracotta and olive for your rich depth",
                "Today's light is warm and soft — Lumi leaned into earthy gold and rosewood to make your skin glow."),
            ["Winter"] = ("Crisp contrast", "Jewel tones and true black for your clarity",
                "Today's light is crisp and clear — Lumi leaned into jewel tones and true black for maximum contrast.")
        };
        var t = titles[family];

        return new ClaudeDailyEditDto
        {
            DailyEditTitle = t.Edit,
            DailyEditSubtitle = t.Sub,
            Description = t.Desc,
            MakeupRecs =
            [
                new ClaudeMakeupRecDto { Title = "Everyday radiance", Subtitle = "Soft daytime glow", Icon = "sparkles" },
                new ClaudeMakeupRecDto { Title = "Soft evening", Subtitle = "Warm low-light look", Icon = "sun" },
                new ClaudeMakeupRecDto { Title = "Weekend glow", Subtitle = "Easy natural finish", Icon = "flower" }
            ],
            AccessoryTitles = ["Gold hoops", "Silk scarf", "Leather tote"]
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
                Description = document.DailyEdit.Description,
                ImageUrl = document.DailyEdit.ImageUrl
            },
            MakeupRecs = document.MakeupRecs
                .Select(m => new MakeupRecItem
                    { Title = m.Title, Subtitle = m.Subtitle, Icon = m.Icon, ImageUrl = m.ImageUrl })
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
                .ToList(),
            Colors = document.Colors
                .Select(c => new ColorItem { Name = c.Name, Hex = c.Hex })
                .ToList(),
            MakeupDetails = document.MakeupDetails
                .Select(m => new MakeupDetailItem { Type = m.Type, Value = m.Value, Icon = m.Icon })
                .ToList(),
            Accessories = document.Accessories
                .Select(a => new AccessoryItem { Title = a.Title, ImageUrl = a.ImageUrl })
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