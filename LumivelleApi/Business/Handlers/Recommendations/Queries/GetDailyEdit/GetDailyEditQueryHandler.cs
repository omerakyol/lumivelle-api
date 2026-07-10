using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Recommendations.Queries.GetDailyEdit;

public class GetDailyEditQueryHandler(IBeautyProfileRepository beautyProfileRepository)
    : IRequestHandler<GetDailyEditQueryRequest, IDataResult<DailyEditResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<DailyEditResult>> Handle(
        GetDailyEditQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var profile = await beautyProfileRepository.GetLatestByAccountIdAsync(accountId);

        if (profile == null)
            return new ErrorDataResult<DailyEditResult>(
                new ResultMessage
                {
                    Code = "NOT_FOUND",
                    Description = "Beauty profile not found"
                });

        var family = GetSeasonFamily(profile.Season);
        var result = BuildSeededResult(family);
        result.Season = profile.Season;
        result.Palette = profile.Palette;

        return new SuccessDataResult<DailyEditResult>(result);
    }

    private static string GetSeasonFamily(string season)
    {
        if (season == null) return "Autumn";
        if (season.Contains("Spring")) return "Spring";
        if (season.Contains("Summer")) return "Summer";
        if (season.Contains("Winter")) return "Winter";
        return "Autumn";
    }

    private static DailyEditResult BuildSeededResult(string family)
    {
        var seed = family.ToLowerInvariant();
        var titles = new Dictionary<string, (string Edit, string Sub, string Outfit)>
        {
            ["Spring"] = ("Fresh morning bloom", "Light, luminous tones for your warm brightness",
                "Soft coral layers"),
            ["Summer"] = ("Cool breeze elegance", "Muted rose and dusty blue for your soft coolness",
                "Powder blue linen"),
            ["Autumn"] = ("Golden hour glow", "Warm terracotta and olive for your rich depth",
                "Camel and cream layers"),
            ["Winter"] = ("Crisp contrast", "Jewel tones and true black for your clarity",
                "Emerald statement coat")
        };
        var t = titles[family];

        return new DailyEditResult
        {
            DailyEdit = new DailyEditItem
            {
                Title = t.Edit,
                Subtitle = t.Sub,
                ImageUrl = $"https://picsum.photos/seed/{seed}-edit/800/1000"
            },
            MakeupRecs =
            [
                new MakeupRecItem { Title = "Everyday radiance", ImageUrl = $"https://picsum.photos/seed/{seed}-mk1/400/400" },
                new MakeupRecItem { Title = "Soft evening", ImageUrl = $"https://picsum.photos/seed/{seed}-mk2/400/400" },
                new MakeupRecItem { Title = "Weekend glow", ImageUrl = $"https://picsum.photos/seed/{seed}-mk3/400/400" }
            ],
            OutfitRec = new OutfitRecItem
            {
                Title = t.Outfit,
                Description = "Built around your seasonal palette and saved style preferences.",
                MatchScore = 94,
                ImageUrl = $"https://picsum.photos/seed/{seed}-outfit/400/500"
            },
            Trending =
            [
                new TrendingItem { Title = "Quiet luxury", ImageUrl = $"https://picsum.photos/seed/{seed}-tr1/400/540" },
                new TrendingItem { Title = "Soft glam", ImageUrl = $"https://picsum.photos/seed/{seed}-tr2/400/540" },
                new TrendingItem { Title = "Minimal lines", ImageUrl = $"https://picsum.photos/seed/{seed}-tr3/400/540" },
                new TrendingItem { Title = "Warm neutrals", ImageUrl = $"https://picsum.photos/seed/{seed}-tr4/400/540" }
            ]
        };
    }
}
