using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Posts.Queries.GetPost;

public class GetPostQueryHandler(
    IPostRepository postRepository,
    IPostLikeRepository postLikeRepository,
    ISavedPostRepository savedPostRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetPostQueryRequest, IDataResult<PostResult>>
{
    public async Task<IDataResult<PostResult>> Handle(
        GetPostQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var postId = ObjectId.Parse(request.Id);
        var document = await postRepository.GetByIdAsync(postId);

        if (document == null)
            throw new ApplicationException(Messages.PostNotFound);

        var results = await PostResultBuilder.ToResultsAsync(
            [document], accountId, postLikeRepository, savedPostRepository, accountRepository);

        return new SuccessDataResult<PostResult>(results.Single());
    }
}
