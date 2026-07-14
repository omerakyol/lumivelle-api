using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Wardrobe.Commands.DeleteWardrobeItem;

public class DeleteWardrobeItemCommandHandler(IWardrobeItemRepository wardrobeItemRepository)
    : IRequestHandler<DeleteWardrobeItemCommandRequest, IResult>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IResult> Handle(
        DeleteWardrobeItemCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var itemId = ObjectId.Parse(request.Id);
        var document = await wardrobeItemRepository.GetByIdAsync(itemId);

        if (document == null || document.AccountId != accountId)
            return new ErrorResult(new ResultMessage { Code = "NOT_FOUND", Description = "Item not found" });

        await wardrobeItemRepository.DeleteAsync(itemId);

        return new SuccessResult();
    }
}
