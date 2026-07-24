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

namespace Business.Handlers.Accounts.Commands.ChangePassword;

public class ChangePasswordCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<ChangePasswordCommandRequest, IResult>
{
    [ValidationAspect(typeof(ChangePasswordValidator), Priority = 1)]
    public async Task<IResult> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var currentAccountId = UserInfoExtensions.GetAccountId();

        var account =
            await accountRepository.GetAsync(x => x.Id == currentAccountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var isPasswordMatch = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, account!.Password);
        if (!isPasswordMatch)
            throw new ApplicationException(Messages.PasswordError);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        account.Password = hashedPassword;

        await accountRepository.UpdateAsync(account.Id, account);

        return new SuccessResult(new ResultMessage { Code = Messages.Updated, Description = Messages.Updated });
    }
}