using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.Register;

public class RegisterCommandRequest : IRequest<IResult>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string DeviceId { get; set; } 
    public string FirebaseToken { get; set; }
    public string PublicKey { get; set; }
}