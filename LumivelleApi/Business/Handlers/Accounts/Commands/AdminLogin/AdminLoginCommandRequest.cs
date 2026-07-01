using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.AdminLogin;

public class AdminLoginCommandRequest : IRequest<IDataResult<AdminLoginCommandResult>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}