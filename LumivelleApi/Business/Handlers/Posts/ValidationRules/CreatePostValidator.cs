using Business.Handlers.Posts.Commands.CreatePost;
using FluentValidation;

namespace Business.Handlers.Posts.ValidationRules;

public class CreatePostValidator : AbstractValidator<CreatePostCommandRequest>
{
    public const int MaxImages = 6;
    public const int MaxCaptionLength = 2200;

    public CreatePostValidator()
    {
        RuleFor(x => x.ImageUrls)
            .Must(urls => urls != null && urls.Length > 0)
            .WithMessage("At least one photo is required");
        RuleFor(x => x.ImageUrls)
            .Must(urls => urls == null || urls.Length <= MaxImages)
            .WithMessage($"A post can have at most {MaxImages} photos");
        RuleForEach(x => x.ImageUrls).NotEmpty();
        RuleFor(x => x.Caption).MaximumLength(MaxCaptionLength);
        RuleFor(x => x)
            .Must(x => string.IsNullOrEmpty(x.WardrobeItemId) || string.IsNullOrEmpty(x.OutfitId))
            .WithMessage("A post can be tagged with at most one wardrobe item or outfit, not both");
    }
}
