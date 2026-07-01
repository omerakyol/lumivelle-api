using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.RegisterFirebaseToken;

public class RegisterFirebaseTokenCommandRequest : IRequest<IResult>
{
    public string FirebaseToken { get; set; }
}