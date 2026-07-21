using Business.Handlers.Accounts.Commands.SetupTwoFactor;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class SetupTwoFactorValidator : AbstractValidator<SetupTwoFactorCommandRequest>
{
    public SetupTwoFactorValidator()
    {
        RuleFor(m => m.Email).EmailAddress().WithMessage(Messages.InvalidEmail);
        RuleFor(m => m.Password).Password();
    }
}