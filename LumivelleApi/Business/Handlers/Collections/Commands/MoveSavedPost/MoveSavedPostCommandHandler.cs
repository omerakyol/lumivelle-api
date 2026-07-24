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

namespace Business.Handlers.Collections.Commands.MoveSavedPost;

public class MoveSavedPostCommandHandler(
    ISavedPostRepository savedPostRepository,
    ICollectionRepository collectionRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<MoveSavedPostCommandRequest, IResult>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IResult> Handle(MoveSavedPostCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var postId = ObjectId.Parse(request.PostId);

        var existingSave = await savedPostRepository.GetAsync(postId, accountId);
        if (existingSave == null)
            throw new ApplicationException(Messages.SavedPostNotFound);

        ObjectId? targetCollectionId = null;
        if (!string.IsNullOrEmpty(request.CollectionId) && request.CollectionId != "all-saved")
        {
            var parsedId = ObjectId.Parse(request.CollectionId);
            var collection = await collectionRepository.GetByIdAsync(parsedId);

            if (collection == null || collection.AccountId != accountId)
                throw new ApplicationException(Messages.CollectionNotFound);

            targetCollectionId = parsedId;
        }

        existingSave.CollectionId = targetCollectionId;
        await savedPostRepository.UpdateAsync(existingSave);

        return new SuccessResult();
    }
}
