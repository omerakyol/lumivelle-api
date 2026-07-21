using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Comments.Commands.DeleteComment;

public class DeleteCommentCommandHandler(
    ICommentRepository commentRepository,
    IPostRepository postRepository)
    : IRequestHandler<DeleteCommentCommandRequest, IResult>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IResult> Handle(DeleteCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var commentId = ObjectId.Parse(request.Id);
        var comment = await commentRepository.GetByIdAsync(commentId);

        if (comment == null || comment.AccountId != accountId)
            return new ErrorResult(new ResultMessage { Code = "NOT_FOUND", Description = "Comment not found" });

        await commentRepository.DeleteAsync(commentId);

        var post = await postRepository.GetByIdAsync(comment.PostId);
        if (post != null)
        {
            post.CommentCount = System.Math.Max(0, post.CommentCount - 1);
            await postRepository.UpdateAsync(post);
        }

        return new SuccessResult();
    }
}
