using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Comments.Commands.DeleteComment;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// Standalone comment operations not nested under a post
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseApiController
{
    /// <summary>Delete a comment (owner only) — decrements the parent post's comment count</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] string id)
    {
        var result = await Mediator.Send(new DeleteCommentCommandRequest { Id = id });
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
