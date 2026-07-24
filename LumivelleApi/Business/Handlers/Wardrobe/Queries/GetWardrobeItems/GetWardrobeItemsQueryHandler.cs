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

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeItems;

public class GetWardrobeItemsQueryHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetWardrobeItemsQueryRequest, IDataResult<List<WardrobeItemResult>>>
{
    public async Task<IDataResult<List<WardrobeItemResult>>> Handle(
        GetWardrobeItemsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var documents = await wardrobeItemRepository.GetByAccountIdAsync(accountId, request.Category);

        var results = documents.Select(d => WardrobeItemResult.FromDocument(d)).ToList();

        return new SuccessDataResult<List<WardrobeItemResult>>(results);
    }
}
