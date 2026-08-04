using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Insights.Queries.GetBeautyScore;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
///     Computed insight dashboards (beauty score, etc.)
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class InsightsController : BaseApiController
{
    /// <summary>Composite beauty score with color harmony, wardrobe IQ, look consistency and routine streak sub-scores</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<BeautyScoreResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("beauty-score")]
    public async Task<IActionResult> GetBeautyScore()
    {
        return GetResponse(await Mediator.Send(new GetBeautyScoreQueryRequest()));
    }
}
