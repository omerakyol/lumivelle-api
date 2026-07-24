using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Posts.Queries.GetFeed;

public class GetFeedQueryHandler(
    IPostRepository postRepository,
    IPostLikeRepository postLikeRepository,
    ISavedPostRepository savedPostRepository,
    IAccountRepository accountRepository,
    IWardrobeItemRepository wardrobeItemRepository,
    IOutfitRepository outfitRepository)
    : IRequestHandler<GetFeedQueryRequest, IDataResult<FeedPageResult>>
{
    public async Task<IDataResult<FeedPageResult>> Handle(
        GetFeedQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var cursor = PostResultBuilder.ParseCursor(request.Cursor);

        var rawPosts = await postRepository.GetFeedPageAsync(cursor, PostResultBuilder.PageSize);
        var nextCursor = rawPosts.Count < PostResultBuilder.PageSize
            ? null
            : rawPosts[^1].CreatedAt.ToString("o");

        var filteredPosts = string.IsNullOrEmpty(request.Category)
            ? rawPosts
            : await FilterByCategoryAsync(rawPosts, request.Category, wardrobeItemRepository, outfitRepository);

        var results = await PostResultBuilder.ToResultsAsync(
            filteredPosts, accountId, postLikeRepository, savedPostRepository, accountRepository);

        return new SuccessDataResult<FeedPageResult>(new FeedPageResult { Posts = results, NextCursor = nextCursor });
    }

    private static async Task<List<PostDocument>> FilterByCategoryAsync(
        List<PostDocument> posts,
        string category,
        IWardrobeItemRepository wardrobeItemRepository,
        IOutfitRepository outfitRepository)
    {
        var wardrobeItemIds = posts.Where(p => p.WardrobeItemId.HasValue)
            .Select(p => p.WardrobeItemId!.Value).Distinct().ToList();
        var outfitIds = posts.Where(p => p.OutfitId.HasValue)
            .Select(p => p.OutfitId!.Value).Distinct().ToList();

        var wardrobeItemsById = (await wardrobeItemRepository.GetByIdsAsync(wardrobeItemIds))
            .ToDictionary(i => i.Id);
        var outfits = await outfitRepository.GetByIdsAsync(outfitIds);
        var outfitsById = outfits.ToDictionary(o => o.Id);

        var outfitMemberItemIds = outfits.SelectMany(o => o.ItemIds).Distinct().ToList();
        var outfitMemberItemsById = (await wardrobeItemRepository.GetByIdsAsync(outfitMemberItemIds))
            .ToDictionary(i => i.Id);

        bool MatchesCategory(PostDocument post)
        {
            if (post.WardrobeItemId.HasValue
                && wardrobeItemsById.TryGetValue(post.WardrobeItemId.Value, out var item))
                return item.Category == category;

            if (post.OutfitId.HasValue && outfitsById.TryGetValue(post.OutfitId.Value, out var outfit))
                return outfit.ItemIds.Any(id =>
                    outfitMemberItemsById.TryGetValue(id, out var memberItem) && memberItem.Category == category);

            return false;
        }

        return posts.Where(MatchesCategory).ToList();
    }
}
