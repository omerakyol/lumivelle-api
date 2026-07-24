using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Collections.Commands.MoveSavedPost;
using Business.Handlers.Comments;
using Business.Handlers.Comments.Commands.CreateComment;
using Business.Handlers.Comments.Queries.GetComments;
using Business.Handlers.Posts;
using Business.Handlers.Posts.Commands.CreatePost;
using Business.Handlers.Posts.Commands.DeletePost;
using Business.Handlers.Posts.Commands.ToggleLike;
using Business.Handlers.Posts.Commands.ToggleSave;
using Business.Handlers.Posts.Queries.GetFeed;
using Business.Handlers.Posts.Queries.GetMyPosts;
using Business.Handlers.Posts.Queries.GetPost;
using Business.Handlers.Posts.Queries.GetSavedPosts;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// Post, like, save and comment operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PostsController : BaseApiController
{
    /// <summary>Create a post</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<PostResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Delete a post (owner only) — cascades to its comments, likes and saves</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromRoute] string id)
    {
        var result = await Mediator.Send(new DeletePostCommandRequest { Id = id });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Reverse-chronological feed of every user's posts, optionally filtered by tagged wardrobe category</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<FeedPageResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet]
    public async Task<IActionResult> GetFeed([FromQuery] string cursor = null, [FromQuery] string category = null)
    {
        return GetResponse(await Mediator.Send(new GetFeedQueryRequest { Cursor = cursor, Category = category }));
    }

    /// <summary>Get a single post</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<PostResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new GetPostQueryRequest { Id = id }));
    }

    /// <summary>The current user's own posts, newest first</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<FeedPageResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("mine")]
    public async Task<IActionResult> GetMyPosts([FromQuery] string cursor = null)
    {
        return GetResponse(await Mediator.Send(new GetMyPostsQueryRequest { Cursor = cursor }));
    }

    /// <summary>Posts the current user has saved, newest-saved first</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<FeedPageResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("saved")]
    public async Task<IActionResult> GetSavedPosts([FromQuery] string cursor = null)
    {
        return GetResponse(await Mediator.Send(new GetSavedPostsQueryRequest { Cursor = cursor }));
    }

    /// <summary>Toggle like on a post</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<ToggleLikeResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("{id}/like")]
    public async Task<IActionResult> ToggleLike([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new ToggleLikeCommandRequest { PostId = id }));
    }

    /// <summary>Toggle save on a post (optional CollectionId targets a specific collection when saving)</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<ToggleSaveResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("{id}/save")]
    public async Task<IActionResult> ToggleSave([FromRoute] string id, [FromBody] ToggleSaveCommandRequest request = null)
    {
        request ??= new ToggleSaveCommandRequest();
        request.PostId = id;
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Move a saved post to a different collection ("all-saved" for the default bucket)</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPut("{id}/collection")]
    public async Task<IActionResult> MoveSavedPost([FromRoute] string id, [FromBody] MoveSavedPostCommandRequest request)
    {
        request.PostId = id;
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List a post's comments, oldest first</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<CommentPageResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("{id}/comments")]
    public async Task<IActionResult> GetComments([FromRoute] string id, [FromQuery] string cursor = null)
    {
        return GetResponse(await Mediator.Send(new GetCommentsQueryRequest { PostId = id, Cursor = cursor }));
    }

    /// <summary>Add a comment to a post</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<CommentResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("{id}/comments")]
    public async Task<IActionResult> CreateComment([FromRoute] string id, [FromBody] CreateCommentCommandRequest request)
    {
        request.PostId = id;
        return GetResponse(await Mediator.Send(request));
    }
}
