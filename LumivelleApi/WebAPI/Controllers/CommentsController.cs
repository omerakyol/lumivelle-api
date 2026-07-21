using System.Threading.Tasks;
using Business.Handlers.Comments.Commands.DeleteComment;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Standalone comment operations not nested under a post
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseApiController
{
    /// <summary>Delete a comment (owner only) — decrements the parent post's comment count</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] string id)
    {
        var result = await Mediator.Send(new DeleteCommentCommandRequest { Id = id });
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
