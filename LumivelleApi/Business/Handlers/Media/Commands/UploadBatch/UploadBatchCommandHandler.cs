using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Media.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Media.Commands.UploadBatch;

public class UploadBatchCommandHandler(IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<UploadBatchCommandRequest, IDataResult<UploadBatchCommandResult>>
{
    [ValidationAspect(typeof(UploadBatchValidator), Priority = 1)]
    public async Task<IDataResult<UploadBatchCommandResult>> Handle(
        UploadBatchCommandRequest request,
        CancellationToken cancellationToken)
    {
        var httpRequest = httpContextAccessor.HttpContext?.Request;
        var urls = new string[request.Files.Count];

        for (var i = 0; i < request.Files.Count; i++)
        {
            var saved = await MediaStorage.SaveFileAsync(
                request.Files[i], request.FolderPath, httpRequest, cancellationToken);
            urls[i] = saved.FileUrl;
        }

        return new SuccessDataResult<UploadBatchCommandResult>(new UploadBatchCommandResult { Urls = urls });
    }
}
