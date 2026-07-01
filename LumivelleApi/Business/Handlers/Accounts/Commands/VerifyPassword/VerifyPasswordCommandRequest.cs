using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.VerifyPassword;

public class VerifyPasswordCommandRequest : IRequest<IResult>
{
    public string Password { get; set; }
}