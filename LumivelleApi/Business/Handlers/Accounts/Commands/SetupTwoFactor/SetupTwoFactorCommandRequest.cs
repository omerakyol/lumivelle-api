using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.SetupTwoFactor;

public class SetupTwoFactorCommandRequest : IRequest<IDataResult<SetupTwoFactorCommandResult>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string? DeviceId { get; set; }
}