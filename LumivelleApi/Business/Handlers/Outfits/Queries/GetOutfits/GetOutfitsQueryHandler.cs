using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Wardrobe;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Outfits.Queries.GetOutfits;

public class GetOutfitsQueryHandler(
    IOutfitRepository outfitRepository,
    IWardrobeItemRepository wardrobeItemRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetOutfitsQueryRequest, IDataResult<List<OutfitResult>>>
{
    public async Task<IDataResult<List<OutfitResult>>> Handle(
        GetOutfitsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var outfits = await outfitRepository.GetByAccountIdAsync(accountId);
        var items = await wardrobeItemRepository.GetByAccountIdAsync(accountId, null);
        var itemsById = items.ToDictionary(i => i.Id);

        var results = outfits.Select(o => new OutfitResult
        {
            Id = o.Id.ToString(),
            Name = o.Name,
            Items = o.ItemIds
                .Where(itemsById.ContainsKey)
                .Select(id => WardrobeItemResult.FromDocument(itemsById[id]))
                .ToList(),
            CreatedAt = o.CreatedAt
        }).ToList();

        return new SuccessDataResult<List<OutfitResult>>(results);
    }
}
