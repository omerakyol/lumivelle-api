using Business.Handlers.Comments.Commands.CreateComment;
using FluentValidation;

namespace Business.Handlers.Comments.ValidationRules;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommandRequest>
{
    public const int MaxTextLength = 500;

    public CreateCommentValidator()
    {
        RuleFor(x => x.PostId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MaximumLength(MaxTextLength);
    }
}
