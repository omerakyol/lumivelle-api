using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Enums;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Accounts.Commands.ResetPasswordWithCode;

public class ResetPasswordWithCodeCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<ResetPasswordWithCodeCommandRequest, IResult>
{
    [ValidationAspect(typeof(ResetPasswordWithCodeValidator), Priority = 1)]
    public async Task<IResult> Handle(ResetPasswordWithCodeCommandRequest request, CancellationToken cancellationToken)
    {
        var account =
            await accountRepository.GetAsync(x =>
                x.Email == request.Email && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        if (string.IsNullOrEmpty(account.PasswordResetCode) || account.PasswordResetCode != request.Code)
            throw new ApplicationException(Messages.PasswordResetCodeInvalid);

        if (account.PasswordResetCodeExpiresAt == null || account.PasswordResetCodeExpiresAt < DateTime.UtcNow)
            throw new ApplicationException(Messages.PasswordResetCodeExpired);

        account.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        account.PasswordResetCode = null;
        account.PasswordResetCodeExpiresAt = null;

        await accountRepository.UpdateAsync(account);

        return new SuccessResult(new ResultMessage { Code = Messages.Updated, Description = Messages.Updated });
    }
}
