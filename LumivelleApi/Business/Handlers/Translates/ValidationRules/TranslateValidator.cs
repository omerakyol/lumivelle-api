using Business.Handlers.Translates.Commands;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Translates.ValidationRules;

public class CreateTranslateValidator : AbstractValidator<CreateTranslateCommand>
{
    public CreateTranslateValidator()
    {
        RuleFor(x => x.LanguageCode).NotEmpty().WithMessage(Messages.LanguageCodeEmpty);
        RuleFor(x => x.Value).NotEmpty().WithMessage(Messages.TranslateValueEmpty);
        RuleFor(x => x.Code).NotEmpty().WithMessage(Messages.TranslateCodeEmpty);
    }
}

public class UpdateTranslateValidator : AbstractValidator<UpdateTranslateCommand>
{
    public UpdateTranslateValidator()
    {
        RuleFor(x => x.LanguageCode).NotEmpty().WithMessage(Messages.LanguageCodeEmpty);
        RuleFor(x => x.Value).NotEmpty().WithMessage(Messages.TranslateValueEmpty);
        RuleFor(x => x.Code).NotEmpty().WithMessage(Messages.TranslateCodeEmpty);
    }
}