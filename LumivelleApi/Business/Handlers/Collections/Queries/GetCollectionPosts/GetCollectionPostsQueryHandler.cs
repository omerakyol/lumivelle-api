using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Posts;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Collections.Queries.GetCollectionPosts;

public class GetCollectionPostsQueryHandler(
    ICollectionRepository collectionRepository,
    ISavedPostRepository savedPostRepository,
    IPostRepository postRepository,
    IPostLikeRepository postLikeRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetCollectionPostsQueryRequest, IDataResult<FeedPageResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<FeedPageResult>> Handle(
        GetCollectionPostsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        ObjectId? collectionId = null;

        if (request.CollectionId != "all-saved")
        {
            var parsedId = ObjectId.Parse(request.CollectionId);
            var collection = await collectionRepository.GetByIdAsync(parsedId);

            if (collection == null || collection.AccountId != accountId)
                throw new ApplicationException(Messages.CollectionNotFound);

            collectionId = parsedId;
        }

        var cursor = PostResultBuilder.ParseCursor(request.Cursor);
        var savedPage = await savedPostRepository.GetByAccountAndCollectionPageAsync(
            accountId, collectionId, cursor, PostResultBuilder.PageSize);

        if (savedPage.Count == 0)
            return new SuccessDataResult<FeedPageResult>(new FeedPageResult { Posts = [], NextCursor = null });

        var postsById = (await postRepository.GetByIdsAsync(savedPage.Select(s => s.PostId)))
            .ToDictionary(p => p.Id);

        var orderedPosts = savedPage
            .Where(s => postsById.ContainsKey(s.PostId))
            .Select(s => postsById[s.PostId])
            .ToList();

        var results = await PostResultBuilder.ToResultsAsync(
            orderedPosts, accountId, postLikeRepository, savedPostRepository, accountRepository);

        return new SuccessDataResult<FeedPageResult>(new FeedPageResult
        {
            Posts = results,
            NextCursor = savedPage.Count < PostResultBuilder.PageSize ? null : savedPage[^1].CreatedAt.ToString("o")
        });
    }
}
