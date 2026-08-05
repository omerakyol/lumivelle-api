using Business.Handlers.Accounts.Commands.Login;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.GoogleLogin;

public class GoogleLoginCommandRequest : IRequest<IDataResult<LoginCommandResult>>
{
    // ID token from the native Google Sign-In SDK on the client.
    public string IdToken { get; set; }
    public string DeviceId { get; set; }
}
