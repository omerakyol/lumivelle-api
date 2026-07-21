using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Enums;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using OtpNet;

namespace Business.Handlers.Accounts.Commands.SetupTwoFactor;

public class SetupTwoFactorCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<SetupTwoFactorCommandRequest, IDataResult<SetupTwoFactorCommandResult>>
{
    [ValidationAspect(typeof(SetupTwoFactorValidator), Priority = 1)]
    public async Task<IDataResult<SetupTwoFactorCommandResult>> Handle(SetupTwoFactorCommandRequest request,
        CancellationToken cancellationToken)
    {
        var account =
            await accountRepository.GetAsync(x =>
                x.Email == request.Email && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var isPasswordMatch = BCrypt.Net.BCrypt.Verify(request.Password, account.Password);
        if (!isPasswordMatch)
            throw new ApplicationException(Messages.PasswordError);


        var otpUrl = GenerateOtpAuthUrl(account.Email, account.TwoFactorSecretKey, GlobalConfig.ApplicationName);

        if (account.TwoFactorEnabled && !string.IsNullOrWhiteSpace(account.TwoFactorSecretKey))
            return new SuccessDataResult<SetupTwoFactorCommandResult>(new SetupTwoFactorCommandResult
            {
                QrCode = QrCodeHelper.GenerateQrCodeBase64(otpUrl)
            });

        var secret = KeyGeneration.GenerateRandomKey(20); // 160-bit
        var base32Secret = Base32Encoding.ToString(secret);

        account.TwoFactorEnabled = true;
        await accountRepository.UpdateAsync(account);

        otpUrl = GenerateOtpAuthUrl(account.Email, base32Secret, GlobalConfig.ApplicationName);
        return new SuccessDataResult<SetupTwoFactorCommandResult>(new SetupTwoFactorCommandResult
        {
            QrCode = QrCodeHelper.GenerateQrCodeBase64(otpUrl),
            SecretKey = base32Secret
        });
    }

    private string GenerateOtpAuthUrl(string email, string base32Secret, string issuer)
    {
        var account = Uri.EscapeDataString(email);
        var escapedIssuer = Uri.EscapeDataString(issuer);

        return $"otpauth://totp/{escapedIssuer}:{account}?secret={base32Secret}&issuer={escapedIssuer}";
    }
}