using Business.Handlers.Accounts.Commands.ChangePassword; 
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommandRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(m => m.CurrentPassword).Password();
        RuleFor(m => m.NewPassword).Password();  
        RuleFor(m => m.ConfirmNewPassword)
            .Equal(m => m.NewPassword)
            .WithMessage(Messages.PasswordAndConfirmPasswordNotSame);
    }
}