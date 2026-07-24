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

namespace Business.Handlers.Collections.Commands.DeleteCollection;

public class DeleteCollectionCommandHandler(
    ICollectionRepository collectionRepository,
    ISavedPostRepository savedPostRepository)
    : IRequestHandler<DeleteCollectionCommandRequest, IResult>
{
    public async Task<IResult> Handle(DeleteCollectionCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.Id == "all-saved")
            throw new ApplicationException(Messages.CannotDeleteDefaultCollection);

        var accountId = UserInfoExtensions.GetAccountId();
        var collectionId = ObjectId.Parse(request.Id);
        var document = await collectionRepository.GetByIdAsync(collectionId);

        if (document == null || document.AccountId != accountId)
            throw new ApplicationException(Messages.CollectionNotFound);

        await savedPostRepository.ClearCollectionIdAsync(collectionId);
        await collectionRepository.DeleteAsync(collectionId);

        return new SuccessResult();
    }
}
