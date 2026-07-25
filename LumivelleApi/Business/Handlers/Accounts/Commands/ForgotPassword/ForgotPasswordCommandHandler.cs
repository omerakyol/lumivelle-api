using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Enums;
using Core.Utilities.Mail;
using Core.Utilities.Results;
using Core.Utilities.Toolkit;
using DataAccess.Abstract;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Business.Handlers.Accounts.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler(
    IAccountRepository accountRepository,
    IMailService mailService,
    IConfiguration configuration)
    : IRequestHandler<ForgotPasswordCommandRequest, IResult>
{
    private static readonly TimeSpan CodeLifetime = TimeSpan.FromMinutes(15);

    [ValidationAspect(typeof(ForgotPasswordValidator), Priority = 1)]
    public async Task<IResult> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var account =
            await accountRepository.GetAsync(x =>
                x.Email == request.Email && x.AccountStatus == AccountStatus.Active);

        if (account != null)
        {
            var code = RandomPassword.RandomNumberGenerator().ToString();
            account.PasswordResetCode = code;
            account.PasswordResetCodeExpiresAt = DateTime.UtcNow.Add(CodeLifetime);
            await accountRepository.UpdateAsync(account);

            var emailConfiguration = configuration.GetSection("EmailConfiguration");
            var emailMessage = new EmailMessage
            {
                Subject = "Lumivelle - Password Reset Code",
                Content = $"Your password reset code is {code}. It expires in 15 minutes.",
                ToAddresses = { new EmailAddress { Address = account.Email } },
                FromAddresses =
                {
                    new EmailAddress
                    {
                        Name = emailConfiguration.GetSection("SenderName").Value,
                        Address = emailConfiguration.GetSection("SenderEmail").Value
                    }
                }
            };
            mailService.Send(emailMessage);
        }

        // Always report success regardless of whether the email matched an
        // account, so this endpoint cannot be used to enumerate accounts.
        return new SuccessResult(new ResultMessage
        {
            Code = Messages.PasswordResetEmailSent, Description = Messages.PasswordResetEmailSent
        });
    }
}
