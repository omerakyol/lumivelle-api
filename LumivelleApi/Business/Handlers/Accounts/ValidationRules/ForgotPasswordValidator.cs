using Business.Handlers.Accounts.Commands.ForgotPassword;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommandRequest>
{
    public ForgotPasswordValidator()
    {
        RuleFor(m => m.Email).EmailAddress().WithMessage(Messages.InvalidEmail);
    }
}
