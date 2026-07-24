using System.Collections.Generic;
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
    
    
    //// <summary>Run Claude vision analysis on an uploaded selfie</summary>
    [Consumes("multipart/form-data")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<BeautyProfileResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [RequestSizeLimit(8_000_000)] // 8MB
    [HttpPost("analyze")]
    public async Task<IActionResult> Analyze(IFormFile file)
    {
        return GetResponse(await Mediator.Send(new AnalyzeCommandRequest { File = file })); 
    }
    
    /// <summary>Run Claude vision analysis on an uploaded selfie</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<BeautyProfileResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("analyze")]
    public async Task<IActionResult> Analyze([FromBody] AnalyzeCommandRequest request)
    {
        return GetResponse(await Mediator.Send(request));
    }

    /// <summary>Get the current user's latest beauty profile</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<BeautyProfileResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        return GetResponse(await Mediator.Send(new GetProfileQueryRequest()));
    }

    /// <summary>Get all past analyses, newest first</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<List<BeautyProfileResult>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("history")]
    public async Task<IActionResult> GetHistory()
    {
        return GetResponse(await Mediator.Send(new GetHistoryQueryRequest()));
    }
}