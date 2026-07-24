using Business.Handlers.Accounts.Commands.UpdateProfile;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommandRequest>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.DisplayName).MaximumLength(60);
        RuleFor(x => x.Bio).MaximumLength(280);
    }
}
