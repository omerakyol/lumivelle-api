using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Core.Utilities.Security.Jwt;

public class JwtHelper : ITokenHelper
{
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public IConfiguration Configuration { get; }

    public JwtSecurityToken DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        if (token.StartsWith("Bearer ")) token = token["Bearer ".Length..];

        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken;
    }

    public TAccessToken CreateToken<TAccessToken>(Account account)
        where TAccessToken : IAccessToken, new()
    {
        _accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, account, signingCredentials);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new TAccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration,
            RefreshToken = GenerateRefreshToken()
        };
    }

    public JwtSecurityToken CreateJwtSecurityToken(
        TokenOptions tokenOptions,
        Account account,
        SigningCredentials signingCredentials)
    {
        var jwt = new JwtSecurityToken(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            claims: SetClaims(account),
            signingCredentials: signingCredentials);
        return jwt;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    private static IEnumerable<Claim> SetClaims(Account account)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(account.Id.ToString());
        claims.AddNameUniqueIdentifier(account.Username);
        claims.AddAccountTypeIdentifier(account.AccountType.ToString());
        return claims;
    }
}