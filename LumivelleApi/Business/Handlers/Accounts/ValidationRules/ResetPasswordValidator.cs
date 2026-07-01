using Business.Handlers.Accounts.Commands.ResetPassword;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommandRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(m => m.Password).Password();
        RuleFor(m => m.ConfirmPassword).Password();
        RuleFor(m => m.Password)
            .Equal(m => m.ConfirmPassword)
            .WithMessage(Messages.PasswordAndConfirmPasswordNotSame);
    }
}