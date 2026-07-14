using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Wardrobe.Commands.ToggleFavorite;

public class ToggleFavoriteCommandHandler(IWardrobeItemRepository wardrobeItemRepository)
    : IRequestHandler<ToggleFavoriteCommandRequest, IDataResult<WardrobeItemResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<WardrobeItemResult>> Handle(
        ToggleFavoriteCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var itemId = ObjectId.Parse(request.Id);
        var document = await wardrobeItemRepository.GetByIdAsync(itemId);

        if (document == null || document.AccountId != accountId)
            return new ErrorDataResult<WardrobeItemResult>(
                new ResultMessage { Code = "NOT_FOUND", Description = "Item not found" });

        document.IsFavorite = !document.IsFavorite;
        await wardrobeItemRepository.UpdateAsync(document);

        return new SuccessDataResult<WardrobeItemResult>(WardrobeItemResult.FromDocument(document));
    }
}
