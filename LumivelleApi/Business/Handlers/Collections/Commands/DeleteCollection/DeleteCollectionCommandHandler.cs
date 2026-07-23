using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
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
    [SecuredOperation(Priority = 1)]
    public async Task<IResult> Handle(DeleteCollectionCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.Id == "all-saved")
            return new ErrorResult(
                new ResultMessage { Code = "CANNOT_DELETE_DEFAULT", Description = "The default collection can't be deleted" });

        var accountId = UserInfoExtensions.GetAccountId();
        var collectionId = ObjectId.Parse(request.Id);
        var document = await collectionRepository.GetByIdAsync(collectionId);

        if (document == null || document.AccountId != accountId)
            return new ErrorResult(new ResultMessage { Code = "NOT_FOUND", Description = "Collection not found" });

        await savedPostRepository.ClearCollectionIdAsync(collectionId);
        await collectionRepository.DeleteAsync(collectionId);

        return new SuccessResult();
    }
}
