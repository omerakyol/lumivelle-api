using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
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
    public async Task<IResult> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var postId = ObjectId.Parse(request.Id);
        var document = await postRepository.GetByIdAsync(postId);

        if (document == null || document.AccountId != accountId)
            throw new ApplicationException(Messages.PostNotFound);

        await commentRepository.DeleteAllByPostIdAsync(postId);
        await postLikeRepository.DeleteAllByPostIdAsync(postId);
        await savedPostRepository.DeleteAllByPostIdAsync(postId);
        await postRepository.DeleteAsync(postId);

        return new SuccessResult();
    }
}
