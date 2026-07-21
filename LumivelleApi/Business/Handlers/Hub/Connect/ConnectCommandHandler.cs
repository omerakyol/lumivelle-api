using System.Threading;
using System.Threading.Tasks;
using Business.Helpers;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Hub.Connect;

public class ConnectCommandRequest : IRequest<IResult>
{
}

public class ConnectCommandHandler(
    IAccountRepository accountRepository,
    ISignalRClientHelper signalRClientHelper)
    : IRequestHandler<ConnectCommandRequest, IResult>
{
    public async Task<IResult> Handle(ConnectCommandRequest request, CancellationToken cancellationToken)
    {
        var email = UserInfoExtensions.GetAccountEmail();

        var accounts = await accountRepository.GetListAsync(x =>
            x.Email != email &&
            x.AccountStatus == AccountStatus.Active
        );

        foreach (var account in accounts)
        {
            await signalRClientHelper.SendToUserAsync(account.Email, "user_online", new { email });
        }

        return new SuccessResult();
    }
}