using System.Threading.Tasks;
using Business.Handlers.Posts.Commands.CreatePost;
using Business.Handlers.Posts.Commands.DeletePost;
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

    /// <summary>Delete a post (owner only) — cascades to its comments, likes and saves</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromRoute] string id)
    {
        var result = await Mediator.Send(new DeletePostCommandRequest { Id = id });
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
