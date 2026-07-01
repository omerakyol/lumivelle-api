using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.SetupTwoFactor;

public class SetupTwoFactorCommandRequest : IRequest<IDataResult<SetupTwoFactorCommandResult>>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string? DeviceId { get; set; }
}