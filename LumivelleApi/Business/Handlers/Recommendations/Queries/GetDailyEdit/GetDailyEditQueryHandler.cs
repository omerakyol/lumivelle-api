using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Recommendations.ValidationRules;
using Business.Handlers.Wardrobe;
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

public class GetDailyEditQueryHandler(
    IBeautyProfileRepository beautyProfileRepository,
    IWardrobeItemRepository wardrobeItemRepository,
    IDailyRecommendationRepository dailyRecommendationRepository,
    IDailyEditPresetRepository dailyEditPresetRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetDailyEditQueryRequest, IDataResult<DailyEditResult>>
{
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

        var preset = await PickPresetAsync(accountId, request.LocalDate, profile.Season, profile.Undertone,
            profile.Contrast);

        var result = new DailyEditResult
        {
            Season = profile.Season,
            Palette = paletteHex,
            DailyEdit = new DailyEditItem
            {
                Title = preset.DailyEditTitle,
                Subtitle = preset.DailyEditSubtitle,
                Description = preset.Description,
                ImageUrl = $"https://picsum.photos/seed/{request.LocalDate}-edit/800/1000"
            },
            MakeupRecs = preset.MakeupRecs
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
            Accessories = preset.AccessoryTitles
                .Select((title, i) => new AccessoryItem
                {
                    Title = title,
                    ImageUrl = $"https://picsum.photos/seed/{request.LocalDate}-acc{i}/400/400"
                })
                .ToList()
        };

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

        return new SuccessDataResult<DailyEditResult>(result);
    }

    /// <summary>
    /// Picks one preset from the (season family, undertone, contrast) bucket, stably keyed by
    /// account + day so the same user sees the same preset all day but a different one tomorrow.
    /// Falls back to season-family-only, then to the very first preset in the collection, so a
    /// user with an incomplete/unusual BeautyProfile (missing undertone/contrast) never 404s.
    /// </summary>
    private async Task<DailyEditPresetDocument> PickPresetAsync(
        ObjectId accountId, string localDate, string season, string undertone, string contrast)
    {
        var family = GetSeasonFamily(season);

        var bucket = await dailyEditPresetRepository.GetByBucketAsync(family, undertone, contrast);
        if (bucket.Count == 0)
            bucket = await dailyEditPresetRepository.GetByBucketAsync(family, "Neutral", "Medium");
        if (bucket.Count == 0)
            bucket = await dailyEditPresetRepository.GetListAsync(x => x.SeasonFamily == family);
        if (bucket.Count == 0)
            throw new ApplicationException("No daily-edit presets are seeded for any season family");

        var index = (int)(StableHash($"{accountId}-{localDate}") % (uint)bucket.Count);
        return bucket[index];
    }

    /// <summary>
    /// FNV-1a hash. Deliberately not <see cref="string.GetHashCode()"/> — that's randomized per
    /// process in .NET, so the same account+date could pick a different preset after a restart.
    /// </summary>
    private static uint StableHash(string value)
    {
        const uint offsetBasis = 2166136261;
        const uint prime = 16777619;
        var hash = offsetBasis;
        foreach (var c in value)
        {
            hash ^= c;
            hash *= prime;
        }

        return hash;
    }

    private static OutfitRecItem BuildOutfitRec(List<WardrobeItemDocument> items, string[] palette)
    {
        if (items.Count == 0)
        {
            return new OutfitRecItem
            {
                Title = "Add your wardrobe",
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
}
