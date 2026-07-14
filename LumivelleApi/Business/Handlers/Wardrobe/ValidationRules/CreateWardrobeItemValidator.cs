using System.Linq;
using Business.Handlers.Wardrobe.Commands.CreateWardrobeItem;
using FluentValidation;

namespace Business.Handlers.Wardrobe.ValidationRules;

public class CreateWardrobeItemValidator : AbstractValidator<CreateWardrobeItemCommandRequest>
{
    public static readonly string[] AllowedCategories =
        ["Tops", "Bottoms", "Dresses", "Outerwear", "Shoes", "Accessories"];

    public CreateWardrobeItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Category)
            .NotEmpty()
            .Must(c => AllowedCategories.Contains(c))
            .WithMessage($"Category must be one of: {string.Join(", ", AllowedCategories)}");
        RuleFor(x => x.Colors).NotEmpty().WithMessage("At least one color is required");
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .Must(url => url != null && (url.StartsWith("http://") || url.StartsWith("https://")))
            .WithMessage("ImageUrl must be an absolute http(s) URL");
    }
}
