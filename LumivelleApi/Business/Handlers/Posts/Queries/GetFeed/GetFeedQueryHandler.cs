using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetFeed;

public class GetFeedQueryHandler(
    IPostRepository postRepository,
    IPostLikeRepository postLikeRepository,
    ISavedPostRepository savedPostRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetFeedQueryRequest, IDataResult<FeedPageResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<FeedPageResult>> Handle(
        GetFeedQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var cursor = PostResultBuilder.ParseCursor(request.Cursor);

        var posts = await postRepository.GetFeedPageAsync(cursor, PostResultBuilder.PageSize);
        var page = await PostResultBuilder.BuildPageAsync(
            posts, accountId, postLikeRepository, savedPostRepository, accountRepository);

        return new SuccessDataResult<FeedPageResult>(page);
    }
}
