using System.Threading.Tasks;
using Business.Handlers.Wardrobe.Commands.AnalyzeWardrobeItem;
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
}
