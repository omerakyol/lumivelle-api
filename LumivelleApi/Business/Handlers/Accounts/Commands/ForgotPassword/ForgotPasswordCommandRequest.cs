using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.ForgotPassword;

public class ForgotPasswordCommandRequest : IRequest<IResult>
{
    public string Email { get; set; }
}
