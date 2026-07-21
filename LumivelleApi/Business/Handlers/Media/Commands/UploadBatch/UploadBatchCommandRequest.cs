using System.Collections.Generic;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Media.Commands.UploadBatch;

public class UploadBatchCommandRequest : IRequest<IDataResult<UploadBatchCommandResult>>
{
    public List<IFormFile> Files { get; set; } = [];
    public string FolderPath { get; set; }
}
