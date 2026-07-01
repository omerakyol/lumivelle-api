using Business.Handlers.Accounts.Commands.ChangeAccountStatus;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class ChangeAccountStatusValidator : AbstractValidator<ChangeAccountStatusCommandRequest>
{
    public ChangeAccountStatusValidator()
    {
        RuleFor(m => m.AccountId).NotEmpty().WithMessage(Messages.AccountEmpty);
    }
}