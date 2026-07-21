using Business.Handlers.Accounts.Commands.Register;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class RegisterAccountValidator : AbstractValidator<RegisterCommandRequest>
{
    public RegisterAccountValidator()
    {
        RuleFor(m => m.Email).EmailAddress().WithMessage(Messages.InvalidEmail);
        RuleFor(m => m.Password).Password();
        RuleFor(m => m.ConfirmPassword).Password();
        RuleFor(m => m.Password)
            .Equal(m => m.ConfirmPassword)
            .WithMessage(Messages.PasswordAndConfirmPasswordNotSame);
        RuleFor(m => m.DeviceId).NotEmpty().WithMessage(Messages.DeviceIdEmpty);
    }
}