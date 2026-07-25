using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.ResetPasswordWithCode;

public class ResetPasswordWithCodeCommandRequest : IRequest<IResult>
{
    public string Email { get; set; }
    public string Code { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
