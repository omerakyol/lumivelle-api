using Core.Entities.Dtos.Account;
using Core.Utilities.Security.Jwt;

namespace Business.Handlers.Accounts.Commands.Register;

public class RegisterCommandResult
{
    public AccessToken AccessToken { get; set; }
    public AccountDto Account { get; set; }
}