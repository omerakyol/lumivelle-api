using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Shades;
using Business.Handlers.Shades.Queries.GetShadesByCategory;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Makeup shade catalog operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ShadesController : BaseApiController
{
    /// <summary>Shades in a category (Lips, Cheeks, or Eyes), in catalog order</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<List<ShadeResult>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet]
    public async Task<IActionResult> GetShadesByCategory([FromQuery] string category)
    {
        return GetResponse(await Mediator.Send(new GetShadesByCategoryQueryRequest { Category = category }));
    }
}
