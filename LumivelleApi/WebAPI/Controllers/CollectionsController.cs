using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Collections;
using Business.Handlers.Collections.Commands.CreateCollection;
using Business.Handlers.Collections.Commands.DeleteCollection;
using Business.Handlers.Collections.Queries.GetCollectionPosts;
using Business.Handlers.Collections.Queries.GetCollections;
using Business.Handlers.Posts;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// Saved-post collection operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CollectionsController : BaseApiController
{
    /// <summary>Create a collection</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<CollectionResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost]
    public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Delete a collection (owner only) — its saves move back to the default "All saved" bucket</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCollection([FromRoute] string id)
    {
        var result = await Mediator.Send(new DeleteCollectionCommandRequest { Id = id });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List the caller's collections, always led by the synthesized default "All saved" bucket</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<List<CollectionResult>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet]
    public async Task<IActionResult> GetCollections()
    {
        return GetResponse(await Mediator.Send(new GetCollectionsQueryRequest()));
    }

    /// <summary>List a collection's posts, newest-saved first ("all-saved" for the default bucket)</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<FeedPageResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("{id}/posts")]
    public async Task<IActionResult> GetCollectionPosts([FromRoute] string id, [FromQuery] string cursor = null)
    {
        return GetResponse(await Mediator.Send(new GetCollectionPostsQueryRequest { CollectionId = id, Cursor = cursor }));
    }
}
