using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Outfits.ValidationRules;
using Business.Handlers.Wardrobe;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Outfits.Commands.CreateOutfit;

public class CreateOutfitCommandHandler(
    IOutfitRepository outfitRepository,
    IWardrobeItemRepository wardrobeItemRepository)
    : IRequestHandler<CreateOutfitCommandRequest, IDataResult<OutfitResult>>
{
    [SecuredOperation(Priority = 1)]
    [ValidationAspect(typeof(CreateOutfitValidator), Priority = 2)]
    public async Task<IDataResult<OutfitResult>> Handle(
        CreateOutfitCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var requestedIds = request.ItemIds.Select(ObjectId.Parse).ToArray();

        var allItems = await wardrobeItemRepository.GetByAccountIdAsync(accountId, null);
        var ownedItems = allItems.Where(i => requestedIds.Contains(i.Id)).ToList();

        if (ownedItems.Count != requestedIds.Length)
            return new ErrorDataResult<OutfitResult>(
                new ResultMessage { Code = "NOT_FOUND", Description = "One or more items were not found" });

        var document = new OutfitDocument
        {
            AccountId = accountId,
            Name = request.Name,
            ItemIds = requestedIds
        };

        await outfitRepository.AddAsync(document);

        var result = new OutfitResult
        {
            Id = document.Id.ToString(),
            Name = document.Name,
            Items = ownedItems.Select(WardrobeItemResult.FromDocument).ToList(),
            CreatedAt = document.CreatedAt
        };

        return new SuccessDataResult<OutfitResult>(result);
    }
}
