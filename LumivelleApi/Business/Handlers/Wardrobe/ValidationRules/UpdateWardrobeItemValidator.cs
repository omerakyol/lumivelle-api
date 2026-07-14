using System.Linq;
using Business.Handlers.Wardrobe.Commands.UpdateWardrobeItem;
using FluentValidation;

namespace Business.Handlers.Wardrobe.ValidationRules;

public class UpdateWardrobeItemValidator : AbstractValidator<UpdateWardrobeItemCommandRequest>
{
    public UpdateWardrobeItemValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Category)
            .NotEmpty()
            .Must(c => CreateWardrobeItemValidator.AllowedCategories.Contains(c))
            .WithMessage($"Category must be one of: {string.Join(", ", CreateWardrobeItemValidator.AllowedCategories)}");
        RuleFor(x => x.Colors).NotEmpty().WithMessage("At least one color is required");
    }
}
