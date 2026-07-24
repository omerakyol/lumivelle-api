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

namespace Business.Handlers.Wardrobe.Commands.MarkWorn;

public class MarkWornCommandHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<MarkWornCommandRequest, IDataResult<WardrobeItemResult>>
{
    public async Task<IDataResult<WardrobeItemResult>> Handle(
        MarkWornCommandRequest request,
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

        document.WearCount += 1;
        document.LastWornAt = DateTime.UtcNow;
        await wardrobeItemRepository.UpdateAsync(document);

        return new SuccessDataResult<WardrobeItemResult>(WardrobeItemResult.FromDocument(document));
    }
}
