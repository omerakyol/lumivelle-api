using Business.Handlers.Accounts.Commands.Register;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class RegisterAccountValidator : AbstractValidator<RegisterCommandRequest>
{
    public RegisterAccountValidator()
    { 
        RuleFor(m => m.Username)
            .NotEmpty().WithMessage(Messages.UsernameEmpty)
            .Matches("^[A-Za-z0-9_-]+$") 
            .WithMessage(Messages.InvalidUsername); 
        RuleFor(m => m.Password).Password();
        RuleFor(m => m.ConfirmPassword).Password();
        RuleFor(m => m.Password)
            .Equal(m => m.ConfirmPassword)
            .WithMessage(Messages.PasswordAndConfirmPasswordNotSame);
        RuleFor(m => m.DeviceId).NotEmpty().WithMessage(Messages.DeviceIdEmpty); 
        RuleFor(m => m.PublicKey).NotEmpty().WithMessage(Messages.PublicKeyEmpty);
    }
}