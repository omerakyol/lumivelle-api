using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.AdminLogin;

public class AdminLoginCommandRequest : IRequest<IDataResult<AdminLoginCommandResult>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}