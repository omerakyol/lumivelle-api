using Business.Handlers.Accounts.Commands.DeleteAccount;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class DeleteAccountValidator : AbstractValidator<DeleteAccountCommandRequest>
{
    public DeleteAccountValidator()
    {
        RuleFor(m => m.AccountId).NotEmpty().WithMessage(Messages.AccountEmpty);
    }
}