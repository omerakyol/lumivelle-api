using System.Linq;
using Business.Handlers.Accounts.Commands.UploadFile;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class UploadFileValidator : AbstractValidator<UploadFileCommandRequest>
{
    // Allowed MIME types
    public static readonly string[] AllowedMimeTypes =
    [
        // Most common formats (SVG intentionally excluded: it can carry
        // executable script and is served from /media — stored-XSS risk).
        "image/jpeg", "image/jpg", "image/png", "image/gif",
        "image/bmp", "image/webp",

        // Modern high-efficiency formats (mobile-first)
        "image/heic", "image/heif", // iOS (default camera format)
        "image/heic-sequence", "image/heif-sequence",
        "image/avif", // Android 12+ and iOS 16+

        // iOS Live Photos (contain paired video, sometimes .mov + .heic)
        "image/heic;type=photo",

        // Most common formats (widely supported on Android & iOS)
        "video/mp4", "video/quicktime", // .mp4, .mov
        "video/3gpp", "video/3gpp2", // .3gp, .3g2
        "video/webm", // modern web + Android

        // Additional formats (some Android support, limited iOS)
        "video/x-matroska", "video/mkv",
        "video/x-msvideo", "video/avi",
        "video/x-ms-wmv", "video/wmv",

        // Streaming and legacy formats
        "video/ogg", "application/vnd.apple.mpegurl", // HLS (.m3u8)
        "video/mp2t", // transport stream (.ts)

        // Common formats
        "audio/mpeg", "audio/mp3",
        "audio/mp4", "audio/m4a", "audio/x-m4a",
        "audio/aac",
        "audio/wav", "audio/x-wav", "audio/vnd.wave",

        // Modern & compressed formats
        "audio/ogg", "audio/opus", "audio/ogg; codecs=opus",
        "audio/webm", "audio/webm; codecs=opus",

        // Legacy and voice formats
        "audio/amr", "audio/amr-wb",
        "audio/3gpp", "audio/3gpp2",

        // Lossless and high-quality
        "audio/flac",

        // Apple-specific formats
        "audio/x-caf", "audio/aiff", "audio/x-aiff",

        // MIDI / system tones
        "audio/midi", "audio/x-midi"
    ];

    public UploadFileValidator()
    {
        RuleFor(x => x.File).NotNull().WithMessage(Messages.FileEmpty);
        RuleFor(x => x.FolderPath).NotEmpty().WithMessage(Messages.FolderPathEmpty);
        RuleFor(x => x.File)
            .NotNull().WithMessage(Messages.FileEmpty)
            .Must(file => file.Length > 0).WithMessage(Messages.FileEmpty)
            .Must(file => AllowedMimeTypes.Contains(file.ContentType.ToLower()))
            .WithMessage(Messages.FileTypeNotAllowed);
    }
}