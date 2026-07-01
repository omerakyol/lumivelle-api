using Business.Handlers.Accounts.Commands.CreateAccount;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class CreateAccountValidator : AbstractValidator<CreateAccountCommandRequest>
{
    public CreateAccountValidator()
    {
        RuleFor(m => m.Username).NotEmpty().WithMessage(Messages.UsernameEmpty);
        RuleFor(m => m.Password).Password();
        RuleFor(m => m.ConfirmPassword).Password();
        RuleFor(m => m.Password)
            .Equal(m => m.ConfirmPassword)
            .WithMessage(Messages.PasswordAndConfirmPasswordNotSame);
    }
}