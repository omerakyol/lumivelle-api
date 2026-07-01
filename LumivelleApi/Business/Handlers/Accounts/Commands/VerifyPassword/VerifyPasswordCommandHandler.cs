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

namespace Business.Handlers.Accounts.Commands.VerifyPassword;

public class VerifyPasswordCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<VerifyPasswordCommandRequest, IResult>
{
    [ValidationAspect(typeof(VerifyPasswordValidator), Priority = 1)]
    public async Task<IResult> Handle(VerifyPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var currentAccountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == currentAccountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var isPasswordMatch = BCrypt.Net.BCrypt.Verify(request.Password, account.Password);
        if (!isPasswordMatch)
            throw new ApplicationException(Messages.PasswordError);

        return new SuccessResult();
    }
}