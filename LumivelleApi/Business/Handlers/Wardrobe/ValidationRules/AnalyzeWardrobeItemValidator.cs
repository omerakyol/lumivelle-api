using Business.Handlers.Wardrobe.Commands.AnalyzeWardrobeItem;
using FluentValidation;

namespace Business.Handlers.Wardrobe.ValidationRules;

public class AnalyzeWardrobeItemValidator : AbstractValidator<AnalyzeWardrobeItemCommandRequest>
{
    public AnalyzeWardrobeItemValidator()
    {
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .Must(url => url != null && (url.StartsWith("http://") || url.StartsWith("https://")))
            .WithMessage("ImageUrl must be an absolute http(s) URL");
    }
}
