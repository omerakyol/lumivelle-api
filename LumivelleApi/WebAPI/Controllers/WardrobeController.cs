using System.Threading.Tasks;
using Business.Handlers.Wardrobe.Commands.AnalyzeWardrobeItem;
using Business.Handlers.Wardrobe.Commands.CreateWardrobeItem;
using Business.Handlers.Wardrobe.Commands.DeleteWardrobeItem;
using Business.Handlers.Wardrobe.Commands.UpdateWardrobeItem;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Wardrobe item operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class WardrobeController : BaseApiController
{
    /// <summary>Run Claude vision tagging on an uploaded wardrobe photo (preview, not persisted)</summary>
    [HttpPost("items/analyze")]
    public async Task<IActionResult> AnalyzeItem([FromBody] AnalyzeWardrobeItemCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Save a wardrobe item</summary>
    [HttpPost("items")]
    public async Task<IActionResult> CreateItem([FromBody] CreateWardrobeItemCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Update a wardrobe item</summary>
    [HttpPut("items/{id}")]
    public async Task<IActionResult> UpdateItem(
        [FromRoute] string id, [FromBody] UpdateWardrobeItemCommandRequest request)
    {
        request.Id = id;
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Delete a wardrobe item</summary>
    [HttpDelete("items/{id}")]
    public async Task<IActionResult> DeleteItem([FromRoute] string id)
    {
        var result = await Mediator.Send(new DeleteWardrobeItemCommandRequest { Id = id });
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
