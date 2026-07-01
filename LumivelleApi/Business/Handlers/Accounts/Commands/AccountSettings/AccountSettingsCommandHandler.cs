using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Core.Entities.Dtos.Account;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Mapster;
using MediatR;

namespace Business.Handlers.Accounts.Commands.AccountSettings;

public class AccountSettingsCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<AccountSettingsCommandRequest, IDataResult<AccountDto>>
{
    public async Task<IDataResult<AccountDto>> Handle(AccountSettingsCommandRequest request,
        CancellationToken cancellationToken)
    {
        var currentAccountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == currentAccountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);


        account.EnablePushNotifications = request.EnablePushNotifications;
        
        await accountRepository.UpdateAsync(account.Id, account);

        var accountDto = account.Adapt<AccountDto>();
        return new SuccessDataResult<AccountDto>(accountDto);
    }
}