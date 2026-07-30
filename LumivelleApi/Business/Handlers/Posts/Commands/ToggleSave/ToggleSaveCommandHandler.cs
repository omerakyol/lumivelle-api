using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Business.Handlers.Posts.Commands.ToggleSave;

public class ToggleSaveCommandHandler(
    IPostRepository postRepository,
    ISavedPostRepository savedPostRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<ToggleSaveCommandRequest, IDataResult<ToggleSaveResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<ToggleSaveResult>> Handle(
        ToggleSaveCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var postId = ObjectId.Parse(request.PostId);
        var post = await postRepository.GetByIdAsync(postId);

        if (post == null)
            throw new ApplicationException(Messages.PostNotFound);

        var existing = await savedPostRepository.GetAsync(postId, accountId);
        bool isSaved;

        if (existing != null)
        {
            await savedPostRepository.DeleteAsync(existing.Id, false);
            post.SaveCount = Math.Max(0, post.SaveCount - 1);
            isSaved = false;
        }
        else
        {
            ObjectId? collectionId = !string.IsNullOrEmpty(request.CollectionId) && request.CollectionId != "all-saved"
                ? ObjectId.Parse(request.CollectionId)
                : null;

            try
            {
                await savedPostRepository.AddAsync(new SavedPostDocument
                {
                    PostId = postId,
                    AccountId = accountId,
                    CollectionId = collectionId
                });
                post.SaveCount += 1;
                isSaved = true;
            }
            catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
            {
                isSaved = true;
            }
        }

        await postRepository.UpdateAsync(post);

        return new SuccessDataResult<ToggleSaveResult>(new ToggleSaveResult { IsSavedByMe = isSaved });
    }
}