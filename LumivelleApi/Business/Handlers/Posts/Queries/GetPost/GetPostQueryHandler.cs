using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
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
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<PostResult>> Handle(
        GetPostQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var postId = ObjectId.Parse(request.Id);
        var document = await postRepository.GetByIdAsync(postId);

        if (document == null)
            return new ErrorDataResult<PostResult>(
                new ResultMessage { Code = "NOT_FOUND", Description = "Post not found" });

        var results = await PostResultBuilder.ToResultsAsync(
            [document], accountId, postLikeRepository, savedPostRepository, accountRepository);

        return new SuccessDataResult<PostResult>(results.Single());
    }
}
