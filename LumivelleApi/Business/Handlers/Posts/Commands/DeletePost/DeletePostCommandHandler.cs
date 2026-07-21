using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Posts.Commands.DeletePost;

public class DeletePostCommandHandler(
    IPostRepository postRepository,
    ICommentRepository commentRepository,
    IPostLikeRepository postLikeRepository,
    ISavedPostRepository savedPostRepository)
    : IRequestHandler<DeletePostCommandRequest, IResult>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IResult> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var postId = ObjectId.Parse(request.Id);
        var document = await postRepository.GetByIdAsync(postId);

        if (document == null || document.AccountId != accountId)
            return new ErrorResult(new ResultMessage { Code = "NOT_FOUND", Description = "Post not found" });

        await commentRepository.DeleteAllByPostIdAsync(postId);
        await postLikeRepository.DeleteAllByPostIdAsync(postId);
        await savedPostRepository.DeleteAllByPostIdAsync(postId);
        await postRepository.DeleteAsync(postId);

        return new SuccessResult();
    }
}
