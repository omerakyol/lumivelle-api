using Business.Handlers.Accounts.Commands.SetupTwoFactor;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class SetupTwoFactorValidator : AbstractValidator<SetupTwoFactorCommandRequest>
{
    public SetupTwoFactorValidator()
    {
        RuleFor(m => m.Username).NotEmpty().WithMessage(Messages.UsernameEmpty);
        RuleFor(m => m.Password).Password();
    }
}