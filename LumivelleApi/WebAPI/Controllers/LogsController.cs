using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.AuditLogs.Queries;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Logs details
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class LogsController : BaseApiController
{
    /// <summary>
    /// List AuditLogs
    /// </summary> 
    /// <return>AuditLogs List</return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LogDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        return GetResponseOnlyResultData(await Mediator.Send(new GetAuditLogDtoQuery()));
    }
}