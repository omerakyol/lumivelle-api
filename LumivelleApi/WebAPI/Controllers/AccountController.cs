using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Business.Handlers.Accounts.Commands.ChangePassword;
using Business.Handlers.Accounts.Commands.DeleteAccount;
using Business.Handlers.Accounts.Commands.DeleteProfile;
using Business.Handlers.Accounts.Commands.ForgotPassword;
using Business.Handlers.Accounts.Commands.Login;
using Business.Handlers.Accounts.Commands.RefreshToken;
using Business.Handlers.Accounts.Commands.Register;
using Business.Handlers.Accounts.Commands.RegisterFirebaseToken;
using Business.Handlers.Accounts.Commands.ResetPassword;
using Business.Handlers.Accounts.Commands.ResetPasswordWithCode;
using Business.Handlers.Accounts.Commands.ResetTwoFactor;
using Business.Handlers.Accounts.Commands.SetAccountFlags;
using Business.Handlers.Accounts.Commands.SetupTwoFactor;
using Business.Handlers.Accounts.Commands.UpdateDeviceInformation;
using Business.Handlers.Accounts.Commands.UpdateProfile;
using Business.Handlers.Accounts.Commands.UploadProfilePicture;
using Business.Handlers.Accounts.Commands.VerifyTwoFactor;
using Business.Handlers.Accounts.Queries.GetAccountProfile;
using Business.Handlers.Accounts.Queries.GetAccountPublicProfile;
using Business.Handlers.Follows;
using Business.Handlers.Follows.Commands.ToggleFollow;
using Business.Handlers.Follows.Queries.GetCreatorProfile;
using Core.Entities.Dtos.Account;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// Account operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseApiController
{
    private readonly string _mediaFolder;

    public AccountController(IWebHostEnvironment env)
    {
        _mediaFolder = Path.Combine(env.WebRootPath, "media");
        if (!Directory.Exists(_mediaFolder))
            Directory.CreateDirectory(_mediaFolder);
    }

    /// <summary>
    /// Account login operation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<LoginCommandResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Account setup two-factor operation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<SetupTwoFactorCommandResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("setup-two-factor")]
    public async Task<IActionResult> GenerateTwoFactor([FromBody] SetupTwoFactorCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Account verify two-factor operation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<LoginCommandResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("verify-two-factor")]
    public async Task<IActionResult> VerifyTwoFactor([FromBody] VerifyTwoFactorCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Refresh token operation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RefreshTokenCommandResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Account register operation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RegisterCommandResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Register firebase token operation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns> 
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("register-firebase-token")]
    public async Task<IActionResult> RegisterFirebaseToken([FromBody] RegisterFirebaseTokenCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Update device information operation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns> 
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("update-device-information")]
    public async Task<IActionResult> UpdateDeviceInformation([FromBody] UpdateDeviceInformationCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Reset account password operation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns> 
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Request a password reset code by email
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Reset account password using the code emailed via forgot-password
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("reset-password-with-code")]
    public async Task<IActionResult> ResetPasswordWithCode([FromBody] ResetPasswordWithCodeCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Change account password operation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns> 
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }


    /// <summary>
    /// Reset two factor operation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns> 
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<List<string>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("reset-two-factor")]
    public async Task<IActionResult> ResetTwoFactor([FromBody] ResetTwoFactorCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Get account profile operation
    /// </summary> 
    /// <returns></returns> 
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<AccountProfileDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("get-profile")]
    public async Task<IActionResult> GetProfile()
    {
        var result = await Mediator.Send(new GetAccountProfileQuery());
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }


    /// <summary>
    /// Delete an account
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns> 
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpDelete("delete-account")]
    public async Task<IActionResult> Delete([FromQuery] string accountId)
    {
        var result = await Mediator.Send(new DeleteAccountCommandRequest { AccountId = accountId });
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Delete account profile operation
    /// </summary> 
    /// <param name="request"></param>
    /// <returns></returns> 
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpDelete("delete-profile")]
    public async Task<IActionResult> DeleteProfile([FromQuery] DeleteProfileCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>
    /// Upload profile picture
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns> 
    [Consumes("multipart/form-data")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [RequestSizeLimit(8_000_000)] // 8MB
    [HttpPost("upload-profile-picture")]
    public async Task<IActionResult> UploadProfilePicture(IFormFile file)
    {
        var result = await Mediator.Send(new UploadProfilePictureCommandRequest
            { File = file, FolderPath = _mediaFolder });
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>Get another account's public display info (name, avatar) for use in feed/comment authorship</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<AccountPublicProfileResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("{id}/public-profile")]
    public async Task<IActionResult> GetPublicProfile([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new GetAccountPublicProfileQueryRequest { Id = id }));
    }

    /// <summary>Update the current user's own display name and bio</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommandRequest request)
    {
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>Admin-only: set an account's verified/creator flags</summary>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPut("{id}/flags")]
    public async Task<IActionResult> SetAccountFlags([FromRoute] string id, [FromBody] SetAccountFlagsCommandRequest request)
    {
        request.Id = id;
        var result = await Mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result.Messages);
    }

    /// <summary>Toggle following another account</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<ToggleFollowResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpPost("{id}/follow")]
    public async Task<IActionResult> ToggleFollow([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new ToggleFollowCommandRequest { FolloweeId = id }));
    }

    /// <summary>Get a creator profile: name, bio, style tag, follower count, follow state</summary>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<CreatorProfileResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultMessage>))]
    [HttpGet("{id}/creator-profile")]
    public async Task<IActionResult> GetCreatorProfile([FromRoute] string id)
    {
        return GetResponse(await Mediator.Send(new GetCreatorProfileQueryRequest { Id = id }));
    }
}