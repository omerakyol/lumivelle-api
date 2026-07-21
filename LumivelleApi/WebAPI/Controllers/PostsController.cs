using System.Threading.Tasks;
using Business.Handlers.Posts.Commands.CreatePost;
using Business.Handlers.Posts.Commands.DeletePost;
using Business.Handlers.Posts.Queries.GetFeed;
using Business.Handlers.Posts.Queries.GetPost;
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

    /// <summary>Reverse-chronological feed of every user's posts</summary>
    [HttpGet]
    public async Task<IActionResult> GetFeed([FromQuery] string cursor = null)
    {
        return GetResponse(await Mediator.Send(new GetFeedQueryRequest { Cursor = cursor }));
    }

    /// <summary>Get a single post</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new GetPostQueryRequest { Id = id }));
    }
}
