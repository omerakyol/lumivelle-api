using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Wardrobe;
using Business.Handlers.Wardrobe.Commands.AnalyzeWardrobeItem;
using Business.Handlers.Wardrobe.Commands.CreateWardrobeItem;
using Business.Handlers.Wardrobe.Commands.DeleteWardrobeItem;
using Business.Handlers.Wardrobe.Commands.MarkWorn;
using Business.Handlers.Wardrobe.Commands.ToggleFavorite;
using Business.Handlers.Wardrobe.Commands.UpdateWardrobeItem;
using Business.Handlers.Wardrobe.Queries.GetPaletteGaps;
using Business.Handlers.Wardrobe.Queries.GetWardrobeItem;
using Business.Handlers.Wardrobe.Queries.GetWardrobeItems;
using Business.Handlers.Wardrobe.Queries.GetWardrobeStats;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// Wardrobe item operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class WardrobeController : BaseApiController
{
    /// <summary>Run Claude vision tagging on an uploaded wardrobe photo (preview, not persisted)</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<AnalyzeWardrobeItemResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("items/analyze")]
    public async Task<IActionResult> AnalyzeItem([FromBody] AnalyzeWardrobeItemCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Save a wardrobe item</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<WardrobeItemResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("items")]
    public async Task<IActionResult> CreateItem([FromBody] CreateWardrobeItemCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Update a wardrobe item</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<WardrobeItemResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPut("items/{id}")]
    public async Task<IActionResult> UpdateItem(
        [FromRoute] string id, [FromBody] UpdateWardrobeItemCommandRequest request)
    {
        request.Id = id;
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Delete a wardrobe item</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpDelete("items/{id}")]
    public async Task<IActionResult> DeleteItem([FromRoute] string id)
    {
        var result = await Mediator.Send(new DeleteWardrobeItemCommandRequest { Id = id });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Toggle favorite status</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<WardrobeItemResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("items/{id}/favorite")]
    public async Task<IActionResult> ToggleFavorite([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new ToggleFavoriteCommandRequest { Id = id }));
    }

    /// <summary>Record that the item was worn today</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<WardrobeItemResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("items/{id}/worn")]
    public async Task<IActionResult> MarkWorn([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new MarkWornCommandRequest { Id = id }));
    }

    /// <summary>List the current user's wardrobe items, optionally filtered by category</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<List<WardrobeItemResult>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("items")]
    public async Task<IActionResult> GetItems([FromQuery] string category = null)
    {
        return GetResponse(await Mediator.Send(new GetWardrobeItemsQueryRequest { Category = category }));
    }

    /// <summary>Get a single wardrobe item</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<WardrobeItemResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("items/{id}")]
    public async Task<IActionResult> GetItem([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new GetWardrobeItemQueryRequest { Id = id }));
    }

    /// <summary>Palette colors with no wardrobe item covering them</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<List<PaletteGapResult>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("palette-gaps")]
    public async Task<IActionResult> GetPaletteGaps()
    {
        return GetResponse(await Mediator.Send(new GetPaletteGapsQueryRequest()));
    }

    /// <summary>Wardrobe summary stats: item count, outfit count, average palette match</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<WardrobeStatsResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        return GetResponse(await Mediator.Send(new GetWardrobeStatsQueryRequest()));
    }
}
