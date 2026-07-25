using Business.Handlers.Accounts.Commands.ResetPasswordWithCode;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class ResetPasswordWithCodeValidator : AbstractValidator<ResetPasswordWithCodeCommandRequest>
{
    public ResetPasswordWithCodeValidator()
    {
        RuleFor(m => m.Email).EmailAddress().WithMessage(Messages.InvalidEmail);
        RuleFor(m => m.Code).NotEmpty().WithMessage(Messages.PasswordResetCodeEmpty);
        RuleFor(m => m.NewPassword).Password();
        RuleFor(m => m.ConfirmNewPassword).Password();
        RuleFor(m => m.NewPassword)
            .Equal(m => m.ConfirmNewPassword)
            .WithMessage(Messages.PasswordAndConfirmPasswordNotSame);
    }
}
