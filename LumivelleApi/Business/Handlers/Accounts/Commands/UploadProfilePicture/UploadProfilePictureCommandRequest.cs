using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Accounts.Commands.UploadProfilePicture;

public class UploadProfilePictureCommandRequest : IRequest<IDataResult<string>>
{
    public IFormFile File { get; set; }
    public string FolderPath { get; set; }
}