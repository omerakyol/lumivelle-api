using System.Threading.Tasks;
using Business.Handlers.StylePreferences.Commands.SavePreferences;
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
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] SavePreferencesCommandRequest request)
    {
        return GetResponseOnlyResultMessage(await Mediator.Send(request));
    }
}
