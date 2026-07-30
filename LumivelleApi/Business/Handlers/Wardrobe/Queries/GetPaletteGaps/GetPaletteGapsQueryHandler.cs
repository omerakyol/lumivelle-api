using System;
using System.Collections.Generic;
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

namespace Business.Handlers.Wardrobe.Queries.GetPaletteGaps;

public class GetPaletteGapsQueryHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IBeautyProfileRepository beautyProfileRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetPaletteGapsQueryRequest, IDataResult<List<PaletteGapResult>>>
{
    public async Task<IDataResult<List<PaletteGapResult>>> Handle(
        GetPaletteGapsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var profile = await beautyProfileRepository.GetLatestByAccountIdAsync(accountId);

        if (profile == null || profile.Palette == null || profile.Palette.Length == 0)
            return new SuccessDataResult<List<PaletteGapResult>>([]);

        var items = await wardrobeItemRepository.GetByAccountIdAsync(accountId, null);
        var wardrobeColors = items.SelectMany(i => i.Colors).ToList();

        var gaps = profile.Palette
            .Where(color => !PaletteMatching.IsColorCoveredByWardrobe(color.Hex, wardrobeColors))
            .Select(color => new PaletteGapResult { Color = color })
            .ToList();

        return new SuccessDataResult<List<PaletteGapResult>>(gaps);
    }
}
