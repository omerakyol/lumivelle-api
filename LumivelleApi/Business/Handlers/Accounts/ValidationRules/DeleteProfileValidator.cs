using Business.Handlers.Accounts.Commands.DeleteProfile;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class DeleteProfileValidator : AbstractValidator<DeleteProfileCommandRequest>
{
    public DeleteProfileValidator()
    {
        RuleFor(m => m.Password).Password();
    }
}