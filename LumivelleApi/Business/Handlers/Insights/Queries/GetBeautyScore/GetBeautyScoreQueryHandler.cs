using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Wardrobe;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Insights.Queries.GetBeautyScore;

public class BeautyScoreResult
{
    public int? Composite { get; set; }
    public int? ColorHarmony { get; set; }
    public int? WardrobeIq { get; set; }
    public int? LookConsistency { get; set; }
    public int? RoutineStreak { get; set; }
}

public class GetBeautyScoreQueryRequest : IRequest<IDataResult<BeautyScoreResult>>
{
}

public class GetBeautyScoreQueryHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IOutfitRepository outfitRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetBeautyScoreQueryRequest, IDataResult<BeautyScoreResult>>
{
    private const int LookbackDays = 30;

    public async Task<IDataResult<BeautyScoreResult>> Handle(
        GetBeautyScoreQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var items = await wardrobeItemRepository.GetByAccountIdAsync(accountId, null);
        var outfits = await outfitRepository.GetByAccountIdAsync(accountId);

        var colorHarmony = items.Count == 0
            ? (int?)null
            : (int)Math.Round(items.Average(i => i.PaletteMatchScore));

        var distinctCategories = items.Select(i => i.Category).Distinct().Count();
        var wardrobeIq = items.Count == 0
            ? (int?)null
            : (int)Math.Min(100, distinctCategories * 15 + Math.Min(items.Count, 20) * 2);

        var cutoff = DateTime.UtcNow.AddDays(-LookbackDays);
        var recentOutfits = outfits.Where(o => o.CreatedAt >= cutoff).ToList();
        var lookConsistency = recentOutfits.Count == 0
            ? (int?)null
            : (int)Math.Round(100.0 * recentOutfits.Count(o =>
            {
                var outfitItems = items.Where(i => o.ItemIds.Contains(i.Id)).ToList();
                return outfitItems.Count > 0 &&
                       outfitItems.Average(i => i.PaletteMatchScore) >= PaletteMatching.CoverageThreshold;
            }) / recentOutfits.Count);

        var activityDates = new HashSet<DateOnly>(
            items.Where(i => i.LastWornAt.HasValue).Select(i => DateOnly.FromDateTime(i.LastWornAt!.Value))
                .Concat(outfits.Select(o => DateOnly.FromDateTime(o.CreatedAt))));
        var streakDays = 0;
        var day = DateOnly.FromDateTime(DateTime.UtcNow);
        while (activityDates.Contains(day))
        {
            streakDays++;
            day = day.AddDays(-1);
        }
        var routineStreak = activityDates.Count == 0 ? (int?)null : (int)Math.Min(100, streakDays * 15);

        var subScores = new[] { colorHarmony, wardrobeIq, lookConsistency, routineStreak }
            .Where(s => s.HasValue).Select(s => s!.Value).ToList();
        var composite = subScores.Count == 0 ? (int?)null : (int)Math.Round(subScores.Average());

        var result = new BeautyScoreResult
        {
            Composite = composite,
            ColorHarmony = colorHarmony,
            WardrobeIq = wardrobeIq,
            LookConsistency = lookConsistency,
            RoutineStreak = routineStreak
        };

        return new SuccessDataResult<BeautyScoreResult>(result);
    }
}
