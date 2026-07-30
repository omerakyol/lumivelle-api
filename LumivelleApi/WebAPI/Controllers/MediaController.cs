using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.Commands.UploadFile;
using Business.Handlers.Media.Commands.UploadBatch;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace WebAPI.Controllers;

/// <summary>
/// Media upload operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class MediaController : BaseApiController
{
    private readonly string _mediaFolder;
    private readonly IMediaFileRepository _mediaFileRepository;

    public MediaController(IWebHostEnvironment env, IMediaFileRepository mediaFileRepository)
    {
        _mediaFolder = Path.Combine(env.WebRootPath, "media");
        if (!Directory.Exists(_mediaFolder))
            Directory.CreateDirectory(_mediaFolder);
        _mediaFileRepository = mediaFileRepository;
    }

    /// <summary>Upload an image and get back its public URL</summary>
    [Consumes("multipart/form-data")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<UploadFileCommandResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [RequestSizeLimit(8_000_000)]
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var result = await Mediator.Send(new UploadFileCommandRequest
            { File = file, FolderPath = _mediaFolder });
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>Upload up to 6 images at once (post photos) and get back their public URLs, in order</summary>
    [Consumes("multipart/form-data")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<UploadBatchCommandResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [RequestSizeLimit(48_000_000)]
    [HttpPost("upload-batch")]
    public async Task<IActionResult> UploadBatch(List<IFormFile> files)
    {
        var result = await Mediator.Send(new UploadBatchCommandRequest
            { Files = files, FolderPath = _mediaFolder });
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Stream a file stored in MongoDB GridFS (e.g. a beauty-profile photo) by its id.
    /// Requires auth; only the account that owns the file (if any owner was recorded) may fetch it.
    /// </summary>
    [Produces("application/octet-stream")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("gridfs/{id}")]
    public async Task<IActionResult> GetGridFsFile(string id, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return NotFound();

        try
        {
            var (stream, contentType, fileName, length, ownerId) =
                await _mediaFileRepository.OpenDownloadStreamAsync(objectId, cancellationToken);

            if (ownerId.HasValue && ownerId.Value != UserInfoExtensions.GetAccountId())
            {
                await stream.DisposeAsync();
                return Forbidden<object>(null);
            }

            Response.Headers.CacheControl = "private, max-age=31536000, immutable";
            Response.ContentLength = length;

            return File(stream, contentType, fileName);
        }
        catch (GridFSFileNotFoundException)
        {
            return NotFound();
        }
    }
}
