using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeItem;

public class GetWardrobeItemQueryHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IOutfitRepository outfitRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetWardrobeItemQueryRequest, IDataResult<WardrobeItemResult>>
{
    public async Task<IDataResult<WardrobeItemResult>> Handle(
        GetWardrobeItemQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var itemId = ObjectId.Parse(request.Id);
        var document = await wardrobeItemRepository.GetByIdAsync(itemId);

        if (document == null || document.AccountId != accountId)
            throw new ApplicationException(Messages.WardrobeItemNotFound);

        var outfitCount = await outfitRepository.CountByItemIdAsync(accountId, itemId);

        return new SuccessDataResult<WardrobeItemResult>(
            WardrobeItemResult.FromDocument(document, outfitCount));
    }
}
