using Business.Handlers.Analysis.Commands.Analyze;
using FluentValidation;

namespace Business.Handlers.Analysis.ValidationRules;

public class AnalyzeValidator : AbstractValidator<AnalyzeCommandRequest>
{
    public AnalyzeValidator()
    {
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .Must(url => url != null && (url.StartsWith("http://") || url.StartsWith("https://")))
            .WithMessage("ImageUrl must be an absolute http(s) URL");
    }
}
