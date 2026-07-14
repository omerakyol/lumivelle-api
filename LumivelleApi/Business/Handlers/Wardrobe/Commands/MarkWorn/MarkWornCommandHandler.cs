using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Wardrobe.Commands.MarkWorn;

public class MarkWornCommandHandler(IWardrobeItemRepository wardrobeItemRepository)
    : IRequestHandler<MarkWornCommandRequest, IDataResult<WardrobeItemResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<WardrobeItemResult>> Handle(
        MarkWornCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var itemId = ObjectId.Parse(request.Id);
        var document = await wardrobeItemRepository.GetByIdAsync(itemId);

        if (document == null || document.AccountId != accountId)
            return new ErrorDataResult<WardrobeItemResult>(
                new ResultMessage { Code = "NOT_FOUND", Description = "Item not found" });

        document.WearCount += 1;
        document.LastWornAt = DateTime.UtcNow;
        await wardrobeItemRepository.UpdateAsync(document);

        return new SuccessDataResult<WardrobeItemResult>(WardrobeItemResult.FromDocument(document));
    }
}
