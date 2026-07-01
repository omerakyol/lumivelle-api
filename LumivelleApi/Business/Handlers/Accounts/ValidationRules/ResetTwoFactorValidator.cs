using Business.Handlers.Accounts.Commands.ResetTwoFactor;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class ResetTwoFactorValidator : AbstractValidator<ResetTwoFactorCommandRequest>
{
    public ResetTwoFactorValidator()
    {
        RuleFor(m => m.AccountId).NotEmpty().WithMessage(Messages.AccountEmpty);
    }
}