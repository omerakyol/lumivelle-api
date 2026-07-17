using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.StylePreferences.Commands.SavePreferences;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Style preference operations
/// </summary>
[Route("api/style-preferences")]
[ApiController]
public class StylePreferencesController : BaseApiController
{
    /// <summary>Upsert the current user's style and goal selections</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResultMessage>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] SavePreferencesCommandRequest request)
    {
        return GetResponseOnlyResultMessage(await Mediator.Send(request));
    }
}
