using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.ChangePassword;

public class ChangePasswordCommandRequest : IRequest<IResult>
{ 
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}