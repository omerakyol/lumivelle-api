using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.Commands.UploadFile;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Media;

public static class MediaStorage
{
    public static async Task<UploadFileCommandResult> SaveFileAsync(
        IFormFile file,
        string folderPath,
        HttpRequest httpRequest,
        CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(folderPath);

        var originalExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}_{Guid.NewGuid()}{originalExtension}";
        var finalPath = Path.Combine(folderPath, fileName);

        await using (var fs = new FileStream(finalPath, FileMode.Create))
        {
            await file.CopyToAsync(fs, cancellationToken);
        }

        var fileInfo = new FileInfo(finalPath);
        var fileUrl = $"{BuildBaseUrl(httpRequest)}/media/{fileName}";

        return new UploadFileCommandResult
        {
            ContentType = GetContentType(fileInfo.Extension),
            ContentLength = fileInfo.Length,
            FileName = fileName,
            FileUrl = fileUrl
        };
    }

    public static string BuildBaseUrl(HttpRequest httpRequest)
    {
        var scheme =
            httpRequest?.Headers["X-Forwarded-Proto"].FirstOrDefault()
            ?? httpRequest?.Headers["X-Forwarded-Scheme"].FirstOrDefault()
            ?? httpRequest?.Scheme
            ?? "https";

        if (scheme.Equals("http", StringComparison.OrdinalIgnoreCase))
        {
            scheme = "https";
        }

        return $"{scheme}://{httpRequest?.Host.Value}";
    }

    private static string GetContentType(string extension) =>
        extension switch
        {
            ".m4a" => "audio/mp4",
            ".mp3" => "audio/mpeg",
            ".ogg" => "audio/ogg",
            ".wav" => "audio/wav",
            _ => "application/octet-stream"
        };
}
