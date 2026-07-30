using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using MongoDB.Bson;

namespace Business.Handlers.Posts;

public static class PostResultBuilder
{
    public const int PageSize = 20;

    public static DateTime? ParseCursor(string cursor)
    {
        if (string.IsNullOrEmpty(cursor))
            return null;

        return DateTime.Parse(cursor, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
    }

    public static async Task<List<PostResult>> ToResultsAsync(
        List<PostDocument> posts,
        ObjectId accountId,
        IPostLikeRepository postLikeRepository,
        ISavedPostRepository savedPostRepository,
        IAccountRepository accountRepository,
        IFollowRepository followRepository)
    {
        if (posts.Count == 0)
            return [];

        var postIds = posts.Select(p => p.Id).ToList();

        var likedPostIds = (await postLikeRepository.GetByAccountAndPostIdsAsync(accountId, postIds))
            .Select(l => l.PostId)
            .ToHashSet();

        var savedPostIds = (await savedPostRepository.GetByAccountAndPostIdsAsync(accountId, postIds))
            .Select(s => s.PostId)
            .ToHashSet();

        var authors = await AuthorLookup.GetAuthorsAsync(
            accountRepository, followRepository, accountId, posts.Select(p => p.AccountId));

        return posts
            .Select(p => PostResult.FromDocument(
                p,
                authors.GetValueOrDefault(p.AccountId.ToString()),
                likedPostIds.Contains(p.Id),
                savedPostIds.Contains(p.Id)))
            .ToList();
    }

    public static async Task<FeedPageResult> BuildPageAsync(
        List<PostDocument> posts,
        ObjectId accountId,
        IPostLikeRepository postLikeRepository,
        ISavedPostRepository savedPostRepository,
        IAccountRepository accountRepository,
        IFollowRepository followRepository)
    {
        var results = await ToResultsAsync(
            posts, accountId, postLikeRepository, savedPostRepository, accountRepository, followRepository);

        return new FeedPageResult
        {
            Posts = results,
            NextCursor = posts.Count < PageSize ? null : posts[^1].CreatedAt.ToString("o")
        };
    }
}