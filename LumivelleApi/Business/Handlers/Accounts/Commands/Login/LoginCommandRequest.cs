using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.Login;

public class LoginCommandRequest : IRequest<IDataResult<LoginCommandResult>>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string DeviceId { get; set; } 
    public string PublicKey { get; set; }
}