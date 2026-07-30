using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Outfits.ValidationRules;
using Business.Handlers.Wardrobe;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Outfits.Commands.CreateOutfit;

public class CreateOutfitCommandHandler(
    IOutfitRepository outfitRepository,
    IWardrobeItemRepository wardrobeItemRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<CreateOutfitCommandRequest, IDataResult<OutfitResult>>
{
    [ValidationAspect(typeof(CreateOutfitValidator), Priority = 1)]
    public async Task<IDataResult<OutfitResult>> Handle(
        CreateOutfitCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var requestedIds = request.ItemIds.Select(ObjectId.Parse).ToArray();

        var allItems = await wardrobeItemRepository.GetByAccountIdAsync(accountId, null);
        var ownedItems = allItems.Where(i => requestedIds.Contains(i.Id)).ToList();

        if (ownedItems.Count != requestedIds.Length)
            throw new ApplicationException(Messages.WardrobeItemNotFound);

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
            Items = ownedItems.Select(i => WardrobeItemResult.FromDocument(i)).ToList(),
            CreatedAt = document.CreatedAt
        };

        return new SuccessDataResult<OutfitResult>(result);
    }
}