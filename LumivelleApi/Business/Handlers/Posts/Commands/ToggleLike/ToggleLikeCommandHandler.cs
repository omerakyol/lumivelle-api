using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Business.Handlers.Posts.Commands.ToggleLike;

public class ToggleLikeCommandHandler(
    IPostRepository postRepository,
    IPostLikeRepository postLikeRepository)
    : IRequestHandler<ToggleLikeCommandRequest, IDataResult<ToggleLikeResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<ToggleLikeResult>> Handle(
        ToggleLikeCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var postId = ObjectId.Parse(request.PostId);
        var post = await postRepository.GetByIdAsync(postId);

        if (post == null)
            return new ErrorDataResult<ToggleLikeResult>(
                new ResultMessage { Code = "NOT_FOUND", Description = "Post not found" });

        var existing = await postLikeRepository.GetAsync(postId, accountId);
        bool isLiked;

        if (existing != null)
        {
            await postLikeRepository.DeleteAsync(existing.Id, softDelete: false);
            post.LikeCount = Math.Max(0, post.LikeCount - 1);
            isLiked = false;
        }
        else
        {
            try
            {
                await postLikeRepository.AddAsync(new PostLikeDocument { PostId = postId, AccountId = accountId });
                post.LikeCount += 1;
                isLiked = true;
            }
            catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
            {
                // Lost the race to a concurrent toggle-like call for the same
                // (postId, accountId) pair — the like already exists, so the
                // desired end state ("liked") is already reached.
                isLiked = true;
            }
        }

        await postRepository.UpdateAsync(post);

        return new SuccessDataResult<ToggleLikeResult>(
            new ToggleLikeResult { IsLikedByMe = isLiked, LikeCount = post.LikeCount });
    }
}
