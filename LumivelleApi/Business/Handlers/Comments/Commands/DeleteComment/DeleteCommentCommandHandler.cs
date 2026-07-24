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
using MongoDB.Bson;

namespace Business.Handlers.Comments.Commands.DeleteComment;

public class DeleteCommentCommandHandler(
    ICommentRepository commentRepository,
    IPostRepository postRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<DeleteCommentCommandRequest, IResult>
{
    public async Task<IResult> Handle(DeleteCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var commentId = ObjectId.Parse(request.Id);
        var comment = await commentRepository.GetByIdAsync(commentId);

        if (comment == null || comment.AccountId != accountId)
            throw new ApplicationException(Messages.CommentNotFound);

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
