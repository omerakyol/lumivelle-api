using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.Commands.UploadFile;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Accounts.Commands.UploadProfilePicture;

public class UploadProfilePictureCommandHandler(
    IAccountRepository accountRepository,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<UploadProfilePictureCommandRequest, IDataResult<string>>
{
    public async Task<IDataResult<string>> Handle(UploadProfilePictureCommandRequest request,
        CancellationToken cancellationToken)
    {
        var currentAccountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == currentAccountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var uploadResult =
            await mediator.Send(new UploadFileCommandRequest { File = request.File, FolderPath = request.FolderPath },
                cancellationToken);

        account.PhotoUrl = uploadResult.Data.FileName;
        await accountRepository.UpdateAsync(account);

        var contextRequest = httpContextAccessor.HttpContext?.Request;
        var baseUrl = $"{contextRequest?.Scheme}://{contextRequest?.Host.Value}";
        var fileUrl = $"{baseUrl}/media/{account.PhotoUrl}";

        return new SuccessDataResult<string>(fileUrl);
    }
}