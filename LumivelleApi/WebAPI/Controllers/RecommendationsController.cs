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
    /// <summary>Get today's personalised recommendation for the current user</summary>
    [HttpGet("daily")]
    public async Task<IActionResult> GetDaily([FromQuery] string localDate)
    {
        return GetResponse(await Mediator.Send(new GetDailyEditQueryRequest { LocalDate = localDate }));
    }
}
