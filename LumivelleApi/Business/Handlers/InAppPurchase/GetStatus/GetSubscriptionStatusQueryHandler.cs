using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.InAppPurchase.GetStatus;

public class SubscriptionStatusResult
{
    public string Tier { get; set; }
    public string Platform { get; set; }
    public string ProductId { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool AutoRenewing { get; set; }
}

public class GetSubscriptionStatusQueryRequest : IRequest<IDataResult<SubscriptionStatusResult>>
{
}

public class GetSubscriptionStatusQueryHandler(IAccountRepository accountRepository)
    : IRequestHandler<GetSubscriptionStatusQueryRequest, IDataResult<SubscriptionStatusResult>>
{
    public async Task<IDataResult<SubscriptionStatusResult>> Handle(
        GetSubscriptionStatusQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account = await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var result = new SubscriptionStatusResult
        {
            Tier = account.SubscriptionTier.ToString(),
            Platform = account.SubscriptionPlatform,
            ProductId = account.SubscriptionProductId,
            ExpiresAt = account.SubscriptionExpiresAt,
            AutoRenewing = account.SubscriptionAutoRenewing
        };

        return new SuccessDataResult<SubscriptionStatusResult>(result);
    }
}
