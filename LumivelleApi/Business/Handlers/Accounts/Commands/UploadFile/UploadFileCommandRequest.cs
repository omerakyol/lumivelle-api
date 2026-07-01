using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Accounts.Commands.UploadFile;

public class UploadFileCommandRequest : IRequest<IDataResult<UploadFileCommandResult>>
{
    public IFormFile File { get; set; }
    public string FolderPath { get; set; }
}