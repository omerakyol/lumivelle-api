using Business.Handlers.Accounts.Commands.VerifyTwoFactor;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class VerifyTwoFactorValidator : AbstractValidator<VerifyTwoFactorCommandRequest>
{
    public VerifyTwoFactorValidator()
    {
        RuleFor(m => m.Username).NotEmpty().WithMessage(Messages.UsernameEmpty);
        RuleFor(m => m.Password).Password();
        RuleFor(m => m.Code).NotEmpty().WithMessage(Messages.OtpCodeEmpty);
    }
}