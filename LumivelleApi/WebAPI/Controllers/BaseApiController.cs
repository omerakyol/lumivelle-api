using System.Collections.Generic;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers;

/// <summary>
/// Base controller
/// </summary>
[Authorize]
[Route("[controller]")]
[ApiController]
public class BaseApiController : Controller
{
    private IMediator _mediator;

    /// <summary>
    /// It is for getting the Mediator instance creation process from the base controller.
    /// </summary>
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponse<T>(IDataResult<T> result)
    {
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponseOnlyResultMessage(IResult result)
    {
        return result.Success ? Ok(result.Messages) : BadRequest(result.Messages);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponseOnlyCreatedResultMessage<T>(IDataResult<T> result)
    {
        return result.Success ? Created<T>(result.Messages, null, result.Data) : BadRequest(result.Messages);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponseOnlyResultData<T>(IDataResult<T> result)
    {
        return result.Success ? Ok(result.Data) : BadRequest(result.Messages);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messages"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Success<T>(List<ResultMessage> messages, string internalMessage, T data)
    {
        return Success(new ApiResult<T>
        {
            Success = true,
            Messages = messages,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Success<T>(ApiResult<T> data)
    {
        return Ok(data);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messages"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Created<T>(List<ResultMessage> messages, string internalMessage, T data)
    {
        return Created(new ApiResult<T>
        {
            Success = true,
            Messages = messages,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Created<T>(ApiResult<T> data)
    {
        return StatusCode(201, data);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messages"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult NoContent<T>(List<ResultMessage> messages, string internalMessage, T data)
    {
        return NoContent(new ApiResult<T>
        {
            Success = true,
            Messages = messages,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult NoContent<T>(ApiResult<T> data)
    {
        return StatusCode(204, data);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messages"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult BadRequest<T>(List<ResultMessage> messages, string internalMessage, T data)
    {
        return BadRequest(new ApiResult<T>
        {
            Success = false,
            Messages = messages,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult BadRequest<T>(ApiResult<T> data)
    {
        return StatusCode(400, data);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messages"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Unauthorized<T>(List<ResultMessage> messages, string internalMessage, T data)
    {
        return Unauthorized(new ApiResult<T>
        {
            Success = false,
            Messages = messages,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Unauthorized<T>(ApiResult<T> data)
    {
        return StatusCode(401, data);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messages"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Forbidden<T>(List<ResultMessage> messages, string internalMessage, T data)
    {
        return Forbidden(new ApiResult<T>
        {
            Success = false,
            Messages = messages,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Forbidden<T>(ApiResult<T> data)
    {
        return StatusCode(403, data);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messages"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult NotFound<T>(List<ResultMessage> messages, string internalMessage, T data)
    {
        return NotFound(new ApiResult<T>
        {
            Success = false,
            Messages = messages,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult NotFound<T>(ApiResult<T> data)
    {
        return StatusCode(404, data);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messages"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Error<T>(List<ResultMessage> messages, string internalMessage, T data)
    {
        return Error(new ApiResult<T>
        {
            Success = false,
            Messages = messages,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Error<T>(ApiResult<T> data)
    {
        return StatusCode(500, data);
    }
}