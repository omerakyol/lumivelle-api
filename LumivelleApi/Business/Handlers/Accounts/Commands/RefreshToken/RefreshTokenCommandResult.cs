using Core.Utilities.Security.Jwt;

namespace Business.Handlers.Accounts.Commands.RefreshToken;

public class RefreshTokenCommandResult
{
    public AccessToken AccessToken { get; set; }
}