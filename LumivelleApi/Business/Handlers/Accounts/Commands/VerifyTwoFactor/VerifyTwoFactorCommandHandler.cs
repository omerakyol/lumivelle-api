using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.Commands.Login;
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
using OtpNet;

namespace Business.Handlers.Accounts.Commands.VerifyTwoFactor;

public class SetupTwoFactorCommandHandler(
    IAccountRepository accountRepository,
    ITokenHelper tokenHelper)
    : IRequestHandler<VerifyTwoFactorCommandRequest, IResult>
{
    [ValidationAspect(typeof(VerifyTwoFactorValidator), Priority = 1)]
    public async Task<IResult> Handle(VerifyTwoFactorCommandRequest request, CancellationToken cancellationToken)
    {
        var account =
            await accountRepository.GetAsync(x =>
                x.Username == request.Username && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var isPasswordMatch = BCrypt.Net.BCrypt.Verify(request.Password, account.Password);
        if (!isPasswordMatch)
            throw new ApplicationException(Messages.PasswordError);


        if (!account.TwoFactorEnabled)
            throw new ApplicationException(Messages.TwoFactorNotSetup);

        var accountTwoFactorSecretKey = account.TwoFactorSecretKey;
        if (string.IsNullOrWhiteSpace(accountTwoFactorSecretKey)) accountTwoFactorSecretKey = request.SecretKey;

        var totp = new Totp(Base32Encoding.ToBytes(accountTwoFactorSecretKey));
        var isValid = totp.VerifyTotp(request.Code, out var timeStepMatched,
            VerificationWindow.RfcSpecifiedNetworkDelay);

        if (!isValid)
            throw new ApplicationException(Messages.OtpCodeInvalid);

        account.TwoFactorSecretKey = accountTwoFactorSecretKey;
        account.Last2FaVerifiedAt = DateTime.UtcNow;
        await accountRepository.UpdateAsync(account);

        var accountDto = account.Adapt<AccountProfileDto>();

        var accessToken = tokenHelper.CreateToken<AccessToken>(account);
        var data = new LoginCommandResult
        {
            AccessToken = accessToken,
            Account = accountDto
        };

        return new SuccessDataResult<LoginCommandResult>(data);
    }
}