using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.ResetPassword;

public class ResetPasswordCommandRequest : IRequest<IResult>
{
    public string AccountId { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}