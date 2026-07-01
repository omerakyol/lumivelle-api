using Business.Handlers.Accounts.Commands.Login;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class VerifyPasswordValidator : AbstractValidator<LoginCommandRequest>
{
    public VerifyPasswordValidator()
    {
        RuleFor(m => m.Password).Password();
    }
}