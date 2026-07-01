using System.IdentityModel.Tokens.Jwt;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt;

public interface ITokenHelper
{
    TAccessToken CreateToken<TAccessToken>(Account user)
        where TAccessToken : IAccessToken, new();

    JwtSecurityToken DecodeToken(string token);

    string GenerateRefreshToken();
}