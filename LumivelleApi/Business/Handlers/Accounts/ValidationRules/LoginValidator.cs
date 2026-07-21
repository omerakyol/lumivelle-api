using Business.Handlers.Accounts.Commands.AdminLogin;
using Business.Handlers.Accounts.Commands.Login;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class LoginValidator : AbstractValidator<LoginCommandRequest>
{
    public LoginValidator()
    {
        RuleFor(m => m.Email).EmailAddress().WithMessage(Messages.InvalidEmail);
        RuleFor(m => m.Password).Password();
        RuleFor(m => m.DeviceId).NotEmpty().WithMessage(Messages.DeviceIdEmpty); 
    }
}

public class AdminLoginValidator : AbstractValidator<AdminLoginCommandRequest>
{
    public AdminLoginValidator()
    {
        RuleFor(m => m.Email).EmailAddress().WithMessage(Messages.InvalidEmail);
        RuleFor(m => m.Password).Password();
    }
}