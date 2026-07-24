using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Posts.Queries.GetAccountPosts;

public class GetAccountPostsQueryHandler(
    IPostRepository postRepository,
    IPostLikeRepository postLikeRepository,
    ISavedPostRepository savedPostRepository,
    IAccountRepository accountRepository,
    IFollowRepository followRepository)
    : IRequestHandler<GetAccountPostsQueryRequest, IDataResult<FeedPageResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<FeedPageResult>> Handle(
        GetAccountPostsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var viewerAccountId = UserInfoExtensions.GetAccountId();
        var targetAccountId = ObjectId.Parse(request.AccountId);
        var cursor = PostResultBuilder.ParseCursor(request.Cursor);

        var posts = await postRepository.GetByAccountIdPageAsync(targetAccountId, cursor, PostResultBuilder.PageSize);
        var page = await PostResultBuilder.BuildPageAsync(
            posts, viewerAccountId, postLikeRepository, savedPostRepository, accountRepository, followRepository);

        return new SuccessDataResult<FeedPageResult>(page);
    }
}
