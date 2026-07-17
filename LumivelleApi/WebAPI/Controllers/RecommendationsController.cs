using System.Threading.Tasks;
using Business.Handlers.Recommendations.Queries.GetDailyEdit;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
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
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<DailyEditResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<DailyEditResult>))]
    [HttpGet("daily")]
    public async Task<IActionResult> GetDaily([FromQuery] string localDate)
    {
        return GetResponse(await Mediator.Send(new GetDailyEditQueryRequest { LocalDate = localDate }));
    }
}
