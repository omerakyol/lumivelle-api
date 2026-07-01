using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Accounts.Commands.UploadFile;

public class UploadFileCommandHandler(
    IAccountRepository accountRepository,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<UploadFileCommandRequest, IDataResult<UploadFileCommandResult>>
{
    [ValidationAspect(typeof(UploadFileValidator), Priority = 1)]
    public async Task<IDataResult<UploadFileCommandResult>> Handle(
        UploadFileCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account = await accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var contextRequest = httpContextAccessor.HttpContext?.Request;
        var file = request.File;
        var folderPath = request.FolderPath;

        Directory.CreateDirectory(folderPath);

        var originalExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        var fileName =
            $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}_{Guid.NewGuid()}{originalExtension}";

        var finalPath = Path.Combine(folderPath, fileName);

        await using var fs = new FileStream(finalPath, FileMode.Create);
        await file.CopyToAsync(fs, cancellationToken);

        var fileInfo = new FileInfo(finalPath);

        var scheme =
            contextRequest?.Headers["X-Forwarded-Proto"].FirstOrDefault()
            ?? contextRequest?.Headers["X-Forwarded-Scheme"].FirstOrDefault()
            ?? contextRequest?.Scheme
            ?? "https";

        if (scheme.Equals("http", StringComparison.OrdinalIgnoreCase))
        {
            scheme = "https";
        }

        var baseUrl = $"{scheme}://{contextRequest?.Host.Value}";
        var fileUrl = $"{baseUrl}/media/{fileName}";

        var data = new UploadFileCommandResult
        {
            ContentType = GetContentType(fileInfo.Extension),
            ContentLength = fileInfo.Length,
            FileName = fileName,
            FileUrl = fileUrl
        };

        return new SuccessDataResult<UploadFileCommandResult>(data);
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