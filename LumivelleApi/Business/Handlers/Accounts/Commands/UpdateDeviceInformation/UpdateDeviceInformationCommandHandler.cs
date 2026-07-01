using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;

using Core.Aspects.Autofac.Validation;
using Core.Constants;

using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Accounts.Commands.UpdateDeviceInformation;

public class UpdateDeviceInformationCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<UpdateDeviceInformationCommandRequest, IResult>
{
    [ValidationAspect(typeof(UpdateDeviceInformationValidator), Priority = 1)]

    public async Task<IResult> Handle(UpdateDeviceInformationCommandRequest request,
        CancellationToken cancellationToken)
    {
        var currentAccountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == currentAccountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        account.DeviceInformation = request.DeviceInformation;
        await accountRepository.UpdateAsync(account);

        return new SuccessResult();
    }
}