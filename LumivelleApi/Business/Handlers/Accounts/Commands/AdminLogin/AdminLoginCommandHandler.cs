using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Entities.Dtos.Account;
using Core.Enums;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Mapster;
using MediatR;

namespace Business.Handlers.Accounts.Commands.AdminLogin;

public class AdminLoginCommandHandler(
    IAccountRepository accountRepository,
    ITokenHelper tokenHelper)
    : IRequestHandler<AdminLoginCommandRequest, IDataResult<AdminLoginCommandResult>>
{
    [ValidationAspect(typeof(AdminLoginValidator), Priority = 1)]
    public async Task<IDataResult<AdminLoginCommandResult>> Handle(AdminLoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        var account =
            await accountRepository.GetAsync(x =>
                x.Email == request.Email && x.AccountStatus == AccountStatus.Active &&
                x.AccountType == AccountType.Admin);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var isPasswordMatch = BCrypt.Net.BCrypt.Verify(request.Password, account.Password);
        if (!isPasswordMatch)
            throw new ApplicationException(Messages.PasswordError);

        if (account.TwoFactorEnabled)
            return string.IsNullOrWhiteSpace(account.TwoFactorSecretKey)
                ? throw new ApplicationException(Messages.TwoFactorNotSetup)
                : throw new ApplicationException(Messages.TwoFactorVerifyRequired);

        var accessToken = tokenHelper.CreateToken<AccessToken>(account);
        var data = new AdminLoginCommandResult
        {
            AccessToken = accessToken,
            Account = account.Adapt<AccountDto>()
        };
        return new SuccessDataResult<AdminLoginCommandResult>(data);
    }
}