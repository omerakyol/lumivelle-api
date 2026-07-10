using System.IO;
using System.Threading.Tasks;
using Business.Handlers.Accounts.Commands.UploadFile;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Media upload operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class MediaController : BaseApiController
{
    private readonly string _mediaFolder;

    public MediaController(IWebHostEnvironment env)
    {
        _mediaFolder = Path.Combine(env.WebRootPath, "media");
        if (!Directory.Exists(_mediaFolder))
            Directory.CreateDirectory(_mediaFolder);
    }

    /// <summary>Upload an image and get back its public URL</summary>
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(8_000_000)]
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var result = await Mediator.Send(new UploadFileCommandRequest
            { File = file, FolderPath = _mediaFolder });
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }
}
