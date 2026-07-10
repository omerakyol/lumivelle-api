using System.Threading.Tasks;
using Business.Handlers.Recommendations.Queries.GetDailyEdit;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Recommendation operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RecommendationsController : BaseApiController
{
    /// <summary>Get the season-seeded daily edit for the current user</summary>
    [HttpGet("daily")]
    public async Task<IActionResult> GetDaily()
    {
        return GetResponse(await Mediator.Send(new GetDailyEditQueryRequest()));
    }
}
