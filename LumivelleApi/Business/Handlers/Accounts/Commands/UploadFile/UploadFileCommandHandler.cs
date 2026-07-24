using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;
using Business.Handlers.Media;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Enums;
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
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var data = await MediaStorage.SaveFileAsync(
            request.File, request.FolderPath, httpContextAccessor.HttpContext?.Request, cancellationToken);

        return new SuccessDataResult<UploadFileCommandResult>(data);
    }
}
