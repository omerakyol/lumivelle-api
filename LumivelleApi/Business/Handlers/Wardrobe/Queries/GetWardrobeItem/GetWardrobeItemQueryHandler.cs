using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeItem;

public class GetWardrobeItemQueryHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IOutfitRepository outfitRepository)
    : IRequestHandler<GetWardrobeItemQueryRequest, IDataResult<WardrobeItemResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<WardrobeItemResult>> Handle(
        GetWardrobeItemQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var itemId = ObjectId.Parse(request.Id);
        var document = await wardrobeItemRepository.GetByIdAsync(itemId);

        if (document == null || document.AccountId != accountId)
            return new ErrorDataResult<WardrobeItemResult>(
                new ResultMessage { Code = "NOT_FOUND", Description = "Item not found" });

        var outfitCount = await outfitRepository.CountByItemIdAsync(accountId, itemId);

        return new SuccessDataResult<WardrobeItemResult>(
            WardrobeItemResult.FromDocument(document, outfitCount));
    }
}
