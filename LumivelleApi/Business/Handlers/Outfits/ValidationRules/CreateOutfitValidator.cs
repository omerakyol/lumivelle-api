using Business.Handlers.Outfits.Commands.CreateOutfit;
using FluentValidation;

namespace Business.Handlers.Outfits.ValidationRules;

public class CreateOutfitValidator : AbstractValidator<CreateOutfitCommandRequest>
{
    public CreateOutfitValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ItemIds)
            .Must(ids => ids != null && ids.Length >= 2)
            .WithMessage("An outfit needs at least 2 items");
    }
}
