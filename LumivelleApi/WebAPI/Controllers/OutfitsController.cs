using System.Threading.Tasks;
using Business.Handlers.Outfits.Commands.CreateOutfit;
using Business.Handlers.Outfits.Commands.DeleteOutfit;
using Business.Handlers.Outfits.Queries.GetOutfits;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Outfit operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OutfitsController : BaseApiController
{
    /// <summary>List the current user's saved outfits</summary>
    [HttpGet]
    public async Task<IActionResult> GetOutfits()
    {
        return GetResponse(await Mediator.Send(new GetOutfitsQueryRequest()));
    }

    /// <summary>Save an outfit combining wardrobe items</summary>
    [HttpPost]
    public async Task<IActionResult> CreateOutfit([FromBody] CreateOutfitCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Delete an outfit</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOutfit([FromRoute] string id)
    {
        var result = await Mediator.Send(new DeleteOutfitCommandRequest { Id = id });
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
