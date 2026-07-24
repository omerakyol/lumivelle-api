using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetMyPosts;

public class GetMyPostsQueryHandler(
    IPostRepository postRepository,
    IPostLikeRepository postLikeRepository,
    ISavedPostRepository savedPostRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetMyPostsQueryRequest, IDataResult<FeedPageResult>>
{
    public async Task<IDataResult<FeedPageResult>> Handle(
        GetMyPostsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var cursor = PostResultBuilder.ParseCursor(request.Cursor);

        var posts = await postRepository.GetByAccountIdPageAsync(accountId, cursor, PostResultBuilder.PageSize);
        var page = await PostResultBuilder.BuildPageAsync(
            posts, accountId, postLikeRepository, savedPostRepository, accountRepository);

        return new SuccessDataResult<FeedPageResult>(page);
    }
}
