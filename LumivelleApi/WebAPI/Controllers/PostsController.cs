using System.Threading.Tasks;
using Business.Handlers.Posts.Commands.CreatePost;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Post, like, save and comment operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PostsController : BaseApiController
{
    /// <summary>Create a post</summary>
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }
}
