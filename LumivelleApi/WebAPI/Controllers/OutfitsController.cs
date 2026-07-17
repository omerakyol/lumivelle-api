using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Outfits;
using Business.Handlers.Outfits.Commands.CreateOutfit;
using Business.Handlers.Outfits.Commands.DeleteOutfit;
using Business.Handlers.Outfits.Queries.GetOutfits;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// Outfit operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OutfitsController : BaseApiController
{
    /// <summary>List the current user's saved outfits</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<List<OutfitResult>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<List<OutfitResult>>))]
    [HttpGet]
    public async Task<IActionResult> GetOutfits()
    {
        return GetResponse(await Mediator.Send(new GetOutfitsQueryRequest()));
    }

    /// <summary>Save an outfit combining wardrobe items</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<OutfitResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<OutfitResult>))]
    [HttpPost]
    public async Task<IActionResult> CreateOutfit([FromBody] CreateOutfitCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Delete an outfit</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOutfit([FromRoute] string id)
    {
        var result = await Mediator.Send(new DeleteOutfitCommandRequest { Id = id });
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
