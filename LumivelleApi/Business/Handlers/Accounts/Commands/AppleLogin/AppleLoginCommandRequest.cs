using Business.Handlers.Accounts.Commands.Login;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.AppleLogin;

public class AppleLoginCommandRequest : IRequest<IDataResult<LoginCommandResult>>
{
    // The signed JWT from Apple's native Sign In With Apple flow.
    public string IdentityToken { get; set; }
    public string DeviceId { get; set; }
}
