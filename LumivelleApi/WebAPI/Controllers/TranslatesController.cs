using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Translates.Commands;
using Business.Handlers.Translates.Queries;
using Core.Entities.Concrete;
using Core.Utilities.Results; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace WebAPI.Controllers;

/// <summary>
/// If controller methods are not Authorize, [AllowAnonymous] is used.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TranslatesController : BaseApiController
{
    /// <summary>
    /// Get translates by language
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns> 
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResultMessage>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("languages/{language}")]
    public async Task<IActionResult> GetTranslatesByLang([FromRoute] string? language = null)
    {
        return GetResponseOnlyResultMessage(await Mediator.Send(new GetTranslatesQuery { LanguageCode = language }));
    }


    /// <summary>
    /// It brings the details according to its id.
    /// </summary>
    /// <remarks>bla bla bla </remarks>
    /// <return>Translate List</return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Translate))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(ObjectId id)
    {
        return GetResponseOnlyResultData(await Mediator.Send(new GetTranslateQuery { Id = id }));
    }

    /// <summary>
    /// Add Translate.
    /// </summary>
    /// <param name="createTranslate"></param>
    /// <returns></returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResultMessage>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateTranslateCommand createTranslate)
    {
        return GetResponseOnlyResultMessage(await Mediator.Send(createTranslate));
    }

    /// <summary>
    /// Update Translate.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResultMessage>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateTranslateCommand command)
    {
        return GetResponseOnlyResultMessage(await Mediator.Send(command));
    }

    /// <summary>
    /// Delete Translate.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResultMessage>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        return GetResponseOnlyResultMessage(await Mediator.Send(new DeleteTranslateCommand { Id = id }));
    }
}