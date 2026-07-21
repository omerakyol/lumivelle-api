using System.Threading.Tasks;
using Business.Handlers.Posts.Commands.CreatePost;
using Business.Handlers.Posts.Commands.DeletePost;
using Business.Handlers.Posts.Queries.GetFeed;
using Business.Handlers.Posts.Queries.GetMyPosts;
using Business.Handlers.Posts.Queries.GetPost;
using Business.Handlers.Posts.Queries.GetSavedPosts;
using Business.Handlers.Posts.Commands.ToggleLike;
using Business.Handlers.Posts.Commands.ToggleSave;
using Business.Handlers.Comments.Commands.CreateComment;
using Business.Handlers.Comments.Queries.GetComments;
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

    /// <summary>The current user's own posts, newest first</summary>
    [HttpGet("mine")]
    public async Task<IActionResult> GetMyPosts([FromQuery] string cursor = null)
    {
        return GetResponse(await Mediator.Send(new GetMyPostsQueryRequest { Cursor = cursor }));
    }

    /// <summary>Posts the current user has saved, newest-saved first</summary>
    [HttpGet("saved")]
    public async Task<IActionResult> GetSavedPosts([FromQuery] string cursor = null)
    {
        return GetResponse(await Mediator.Send(new GetSavedPostsQueryRequest { Cursor = cursor }));
    }

    /// <summary>Toggle like on a post</summary>
    [HttpPost("{id}/like")]
    public async Task<IActionResult> ToggleLike([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new ToggleLikeCommandRequest { PostId = id }));
    }

    /// <summary>Toggle save on a post</summary>
    [HttpPost("{id}/save")]
    public async Task<IActionResult> ToggleSave([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new ToggleSaveCommandRequest { PostId = id }));
    }

    /// <summary>List a post's comments, oldest first</summary>
    [HttpGet("{id}/comments")]
    public async Task<IActionResult> GetComments([FromRoute] string id, [FromQuery] string cursor = null)
    {
        return GetResponse(await Mediator.Send(new GetCommentsQueryRequest { PostId = id, Cursor = cursor }));
    }

    /// <summary>Add a comment to a post</summary>
    [HttpPost("{id}/comments")]
    public async Task<IActionResult> CreateComment([FromRoute] string id, [FromBody] CreateCommentCommandRequest request)
    {
        request.PostId = id;
        return GetResponse(await Mediator.Send(request));
    }
}
