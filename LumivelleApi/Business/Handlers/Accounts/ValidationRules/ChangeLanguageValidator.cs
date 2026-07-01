using System.Linq;
using Business.Handlers.Accounts.Commands.ChangeLanguage;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class ChangeLanguageValidator : AbstractValidator<ChangeLanguageCommandRequest>
{
    private readonly string[] _allowedLanguages =
    [
        "en",
        "tr",
        "de",
        "es",
        "fr",
        "nl"
    ];

    public ChangeLanguageValidator()
    {
        RuleFor(m => m.Language).NotEmpty().WithMessage(Messages.LanguageCodeEmpty);
        RuleFor(x => x.Language)
            .NotEmpty().WithMessage(Messages.LanguageCodeEmpty)
            .Must(lang => _allowedLanguages.Contains(lang.ToLower()))
            .WithMessage(Messages.LanguageNotFound);
    }
}