using System.Threading;
using System.Threading.Tasks;
using Business.Helpers;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Hub.Disconnect;

public class DisconnectCommandRequest : IRequest<IResult>
{
}

public class DisconnectCommandHandler(
    IAccountRepository accountRepository,
    ISignalRClientHelper signalRClientHelper)
    : IRequestHandler<DisconnectCommandRequest, IResult>
{
    public async Task<IResult> Handle(DisconnectCommandRequest request, CancellationToken cancellationToken)
    {
        var email = UserInfoExtensions.GetAccountEmail();

        var accounts = await accountRepository.GetListAsync(x =>
            x.Email != email &&
            x.AccountStatus == AccountStatus.Active
        );

        foreach (var account in accounts)
        {
            await signalRClientHelper.SendToUserAsync(account.Email, "user_offline", new { email });
        }

        return new SuccessResult();
    }
}