using System.Linq;
using Business.Handlers.Accounts.ValidationRules;
using Business.Handlers.Media.Commands.UploadBatch;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Media.ValidationRules;

public class UploadBatchValidator : AbstractValidator<UploadBatchCommandRequest>
{
    public const int MaxFiles = 6;

    public UploadBatchValidator()
    {
        RuleFor(x => x.FolderPath).NotEmpty().WithMessage(Messages.FolderPathEmpty);
        RuleFor(x => x.Files)
            .Must(files => files != null && files.Count > 0)
            .WithMessage("At least one file is required");
        RuleFor(x => x.Files)
            .Must(files => files == null || files.Count <= MaxFiles)
            .WithMessage($"At most {MaxFiles} files can be uploaded at once");
        RuleForEach(x => x.Files)
            .Must(file => file != null && file.Length > 0)
            .WithMessage(Messages.FileEmpty)
            .Must(file => file != null && UploadFileValidator.AllowedMimeTypes.Contains(file.ContentType.ToLower()))
            .WithMessage(Messages.FileTypeNotAllowed);
    }
}
