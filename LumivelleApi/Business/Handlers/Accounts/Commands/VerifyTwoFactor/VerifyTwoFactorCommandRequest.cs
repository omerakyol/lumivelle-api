using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.VerifyTwoFactor;

public class VerifyTwoFactorCommandRequest : IRequest<IResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string? DeviceId { get; set; }
    public string Code { get; set; }
    public string? SecretKey { get; set; }
}