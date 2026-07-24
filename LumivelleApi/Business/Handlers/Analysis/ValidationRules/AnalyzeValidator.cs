using System.Linq;
using Business.Handlers.Analysis.Commands.Analyze;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Analysis.ValidationRules;

public class AnalyzeValidator : AbstractValidator<AnalyzeCommandRequest>
{
    public static readonly string[] AllowedMimeTypes =
    [
        "image/jpeg", "image/jpg", "image/png", "image/gif",
        "image/bmp", "image/webp",

        // Modern high-efficiency formats (mobile-first)
        "image/heic", "image/heif",
        "image/heic-sequence", "image/heif-sequence",
        "image/avif",
        "image/heic;type=photo"
    ];

    public AnalyzeValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage(Messages.FileEmpty)
            .Must(file => file.Length > 0).WithMessage(Messages.FileEmpty)
            .Must(file => AllowedMimeTypes.Contains(file.ContentType.ToLower()))
            .WithMessage(Messages.FileTypeNotAllowed);
    }
}