using System.Threading.Tasks;
using Business.Handlers.Wardrobe.Commands.AnalyzeWardrobeItem;
using Business.Handlers.Wardrobe.Commands.CreateWardrobeItem;
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
}
