using System.Threading.Tasks;
using Business.Handlers.Analysis;
using Business.Handlers.Analysis.Commands.Analyze;
using Business.Handlers.Analysis.Queries.GetHistory;
using Business.Handlers.Analysis.Queries.GetProfile;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// AI beauty analysis operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AnalysisController : BaseApiController
{
    /// <summary>Run Claude vision analysis on an uploaded selfie</summary>
    [HttpPost("analyze")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<BeautyProfileResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> Analyze([FromBody] AnalyzeCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Get the current user's latest beauty profile</summary>
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        return GetResponse(await Mediator.Send(new GetProfileQueryRequest()));
    }

    /// <summary>Get all past analyses, newest first</summary>
    [HttpGet("history")]
    public async Task<IActionResult> GetHistory()
    {
        return GetResponse(await Mediator.Send(new GetHistoryQueryRequest()));
    }
}