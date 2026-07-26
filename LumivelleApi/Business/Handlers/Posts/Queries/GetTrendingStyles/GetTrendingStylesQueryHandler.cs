using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Posts.Queries.GetTrendingStyles;

public class GetTrendingStylesQueryHandler(
    IPostRepository postRepository,
    IWardrobeItemRepository wardrobeItemRepository,
    IOutfitRepository outfitRepository)
    : IRequestHandler<GetTrendingStylesQueryRequest, IDataResult<List<TrendResult>>>
{
    private const int TopCount = 10;

    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<List<TrendResult>>> Handle(
        GetTrendingStylesQueryRequest request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        DateTime? currentWindowStart = request.Range switch
        {
            "week" => now.AddDays(-7),
            "month" => now.AddDays(-30),
            _ => null
        };
        DateTime? fetchSince = request.Range switch
        {
            "week" => now.AddDays(-14),
            "month" => now.AddDays(-60),
            _ => null
        };

        var posts = await postRepository.GetActiveInWindowAsync(fetchSince);

        var currentPosts = currentWindowStart.HasValue
            ? posts.Where(p => p.CreatedAt >= currentWindowStart.Value).ToList()
            : posts;
        var priorPosts = currentWindowStart.HasValue
            ? posts.Where(p => p.CreatedAt < currentWindowStart.Value).ToList()
            : [];

        var tagsByPostId = await PostStyleTagResolver.GetTagsByPostIdAsync(posts, wardrobeItemRepository, outfitRepository);

        var currentTotals = SumSavesByTag(currentPosts, tagsByPostId);
        var priorTotals = SumSavesByTag(priorPosts, tagsByPostId);

        var top = currentTotals
            .OrderByDescending(kv => kv.Value)
            .Take(TopCount)
            .Select(kv => new TrendResult
            {
                StyleTag = kv.Key,
                TotalSaves = kv.Value,
                DeltaPercent = currentWindowStart.HasValue ? ComputeDeltaPercent(kv.Value, priorTotals.GetValueOrDefault(kv.Key)) : 0
            })
            .ToList();

        return new SuccessDataResult<List<TrendResult>>(top);
    }

    private static Dictionary<string, int> SumSavesByTag(
        List<PostDocument> posts, Dictionary<ObjectId, HashSet<string>> tagsByPostId)
    {
        var totals = new Dictionary<string, int>();

        foreach (var post in posts)
        {
            if (!tagsByPostId.TryGetValue(post.Id, out var tags))
                continue;

            foreach (var tag in tags)
                totals[tag] = totals.GetValueOrDefault(tag) + post.SaveCount;
        }

        return totals;
    }

    private static int ComputeDeltaPercent(int current, int prior)
    {
        if (prior == 0)
            return current > 0 ? 100 : 0;

        return (int)Math.Round((current - prior) * 100.0 / prior);
    }
}
