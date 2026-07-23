using Business.Handlers.Collections.Commands.CreateCollection;
using FluentValidation;

namespace Business.Handlers.Collections.ValidationRules;

public class CreateCollectionValidator : AbstractValidator<CreateCollectionCommandRequest>
{
    public CreateCollectionValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(60);
    }
}
