using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Collections;
using Business.Handlers.Collections.Commands.CreateCollection;
using Business.Handlers.Collections.Commands.DeleteCollection;
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
}
