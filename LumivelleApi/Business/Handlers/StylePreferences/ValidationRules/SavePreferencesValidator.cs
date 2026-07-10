using Business.Handlers.StylePreferences.Commands.SavePreferences;
using FluentValidation;

namespace Business.Handlers.StylePreferences.ValidationRules;

public class SavePreferencesValidator : AbstractValidator<SavePreferencesCommandRequest>
{
    public SavePreferencesValidator()
    {
        RuleFor(x => x.Styles).NotEmpty().WithMessage("At least one style is required");
        RuleFor(x => x.Goals).NotEmpty().WithMessage("At least one goal is required");
    }
}
