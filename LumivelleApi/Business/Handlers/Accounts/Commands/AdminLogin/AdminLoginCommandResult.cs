using Core.Entities.Dtos.Account;
using Core.Utilities.Security.Jwt;

namespace Business.Handlers.Accounts.Commands.AdminLogin;

public class AdminLoginCommandResult
{
    public AccessToken AccessToken { get; set; }
    public AccountDto Account { get; set; }
}