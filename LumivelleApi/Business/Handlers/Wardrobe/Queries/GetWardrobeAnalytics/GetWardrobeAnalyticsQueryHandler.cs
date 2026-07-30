using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeAnalytics;

public class GetWardrobeAnalyticsQueryHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetWardrobeAnalyticsQueryRequest, IDataResult<WardrobeAnalyticsResult>>
{
    public async Task<IDataResult<WardrobeAnalyticsResult>> Handle(
        GetWardrobeAnalyticsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var items = await wardrobeItemRepository.GetByAccountIdAsync(accountId, null);

        var result = new WardrobeAnalyticsResult
        {
            PaletteAlignmentScore = items.Count == 0
                ? 0
                : (int)Math.Round(items.Average(i => i.PaletteMatchScore)),
            MostWornColors = BuildMostWornColors(items),
            StyleDistribution = BuildStyleDistribution(items),
            SeasonalUsage = BuildSeasonalUsage(items)
        };

        return new SuccessDataResult<WardrobeAnalyticsResult>(result);
    }

    private static List<ColorUsageResult> BuildMostWornColors(List<WardrobeItemDocument> items)
    {
        var buckets = new List<(string Hex, int TotalWear)>();

        foreach (var item in items)
        foreach (var color in item.Colors)
        {
            var index = buckets.FindIndex(b =>
                PaletteMatching.ScoreColorAgainstPalette(color, [b.Hex]) >= PaletteMatching.CoverageThreshold);

            if (index >= 0)
                buckets[index] = (buckets[index].Hex, buckets[index].TotalWear + item.WearCount);
            else
                buckets.Add((color, item.WearCount));
        }

        var top = buckets.OrderByDescending(b => b.TotalWear).Take(4).ToList();
        if (top.Count == 0)
            return [];

        var max = Math.Max(top[0].TotalWear, 1);

        return top.Select(b => new ColorUsageResult
        {
            Hex = b.Hex,
            Label = b.Hex,
            Percentage = (int)Math.Round(b.TotalWear * 100.0 / max)
        }).ToList();
    }

    private static List<StyleUsageResult> BuildStyleDistribution(List<WardrobeItemDocument> items)
    {
        var counts = new Dictionary<string, int>();

        foreach (var tag in items.SelectMany(i => i.StyleTags)) counts[tag] = counts.GetValueOrDefault(tag) + 1;

        var top = counts.OrderByDescending(kv => kv.Value).Take(4).ToList();
        if (top.Count == 0)
            return [];

        var max = top[0].Value;

        return top.Select(kv => new StyleUsageResult
        {
            Tag = kv.Key,
            Percentage = (int)Math.Round(kv.Value * 100.0 / max)
        }).ToList();
    }

    private static List<SeasonUsageResult> BuildSeasonalUsage(List<WardrobeItemDocument> items)
    {
        var seasons = new[] { "Spring", "Summer", "Autumn", "Winter" };
        var counts = seasons.ToDictionary(s => s, _ => 0);

        foreach (var item in items)
        {
            if (item.LastWornAt == null)
                continue;

            counts[ToSeason(item.LastWornAt.Value.Month)] += item.WearCount;
        }

        return seasons.Select(s => new SeasonUsageResult { Season = s, Count = counts[s] }).ToList();
    }

    private static string ToSeason(int month)
    {
        return month switch
        {
            3 or 4 or 5 => "Spring",
            6 or 7 or 8 => "Summer",
            9 or 10 or 11 => "Autumn",
            _ => "Winter"
        };
    }
}