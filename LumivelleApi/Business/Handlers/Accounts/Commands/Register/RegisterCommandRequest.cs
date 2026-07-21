using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.Register;

public class RegisterCommandRequest : IRequest<IResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string DeviceId { get; set; }
    public string FirebaseToken { get; set; }
}