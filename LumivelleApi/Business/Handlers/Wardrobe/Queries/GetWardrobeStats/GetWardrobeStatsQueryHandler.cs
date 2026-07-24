using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeStats;

public class GetWardrobeStatsQueryHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IOutfitRepository outfitRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetWardrobeStatsQueryRequest, IDataResult<WardrobeStatsResult>>
{
    public async Task<IDataResult<WardrobeStatsResult>> Handle(
        GetWardrobeStatsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var items = await wardrobeItemRepository.GetByAccountIdAsync(accountId, null);
        var outfits = await outfitRepository.GetByAccountIdAsync(accountId);

        var onPaletteScore = items.Count == 0
            ? 0
            : (int)Math.Round(items.Average(i => i.PaletteMatchScore));

        var result = new WardrobeStatsResult
        {
            ItemCount = items.Count,
            OutfitCount = outfits.Count,
            OnPaletteScore = onPaletteScore
        };

        return new SuccessDataResult<WardrobeStatsResult>(result);
    }
}
