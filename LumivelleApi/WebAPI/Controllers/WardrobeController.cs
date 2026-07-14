using System.Threading.Tasks;
using Business.Handlers.Wardrobe.Commands.AnalyzeWardrobeItem;
using Business.Handlers.Wardrobe.Commands.CreateWardrobeItem;
using Business.Handlers.Wardrobe.Commands.DeleteWardrobeItem;
using Business.Handlers.Wardrobe.Commands.MarkWorn;
using Business.Handlers.Wardrobe.Commands.ToggleFavorite;
using Business.Handlers.Wardrobe.Commands.UpdateWardrobeItem;
using Business.Handlers.Wardrobe.Queries.GetWardrobeItem;
using Business.Handlers.Wardrobe.Queries.GetWardrobeItems;
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

    /// <summary>Toggle favorite status</summary>
    [HttpPost("items/{id}/favorite")]
    public async Task<IActionResult> ToggleFavorite([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new ToggleFavoriteCommandRequest { Id = id }));
    }

    /// <summary>Record that the item was worn today</summary>
    [HttpPost("items/{id}/worn")]
    public async Task<IActionResult> MarkWorn([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new MarkWornCommandRequest { Id = id }));
    }

    /// <summary>List the current user's wardrobe items, optionally filtered by category</summary>
    [HttpGet("items")]
    public async Task<IActionResult> GetItems([FromQuery] string category = null)
    {
        return GetResponse(await Mediator.Send(new GetWardrobeItemsQueryRequest { Category = category }));
    }

    /// <summary>Get a single wardrobe item</summary>
    [HttpGet("items/{id}")]
    public async Task<IActionResult> GetItem([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new GetWardrobeItemQueryRequest { Id = id }));
    }
}
