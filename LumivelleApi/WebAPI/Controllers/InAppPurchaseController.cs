using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.InAppPurchase.Apple;
using Business.Handlers.InAppPurchase.GetStatus;
using Business.Handlers.InAppPurchase.Google;
using Business.Handlers.Notification.Commands.GetNotification;
using Business.Handlers.Notification.Commands.SendNotification;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// In-app purchase operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class InAppPurchaseController : BaseApiController
{
    /// <summary>
    /// Validate Google In-App Purchase Receipt
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns> 
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("validate-google-receipt")]
    public async Task<IActionResult> ValidateGoogleReceipt([FromBody] ValidateGoogleReceiptCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Validate Apple In App Purchase Receipt
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns> 
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("validate-apple-receipt")]
    public async Task<IActionResult> ValidateAppleReceipt([FromBody] ValidateAppleReceiptCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Get the current account's subscription status
    /// </summary>
    /// <returns></returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<SubscriptionStatusResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("subscription-status")]
    public async Task<IActionResult> GetSubscriptionStatus()
    {
        var result = await Mediator.Send(new GetSubscriptionStatusQueryRequest());
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    // [AllowAnonymous]
    // [Consumes("application/json")]
    // [Produces("application/json", "text/plain")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
    // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    // [HttpPost("send-test-message")]
    // public async Task<IActionResult> SendTestMessage([FromBody] SendNotificationCommandRequest request)
    // {
    //     var result = await Mediator.Send(request);
    //     return result.Success ? Ok(result) : BadRequest(result.Messages);
    // }
    //
    // [AllowAnonymous]
    // [Consumes("application/json")]
    // [Produces("application/json", "text/plain")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
    // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    // [HttpPost("get-test-message")]
    // public async Task<IActionResult> GetTestMessage([FromBody] GetNotificationCommandRequest request)
    // {
    //     var result = await Mediator.Send(request);
    //     return result.Success ? Ok(result) : BadRequest(result.Messages);
    // }
}