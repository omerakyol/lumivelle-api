using Core.Entities.Dtos.Account;
using Core.Utilities.Security.Jwt;

namespace Business.Handlers.Accounts.Commands.Login;

public class LoginCommandResult
{
    public AccessToken AccessToken { get; set; }
    public AccountProfileDto Account { get; set; }
}