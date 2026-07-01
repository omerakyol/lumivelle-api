using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions;

public static class ClaimExtensions
{
    public static string NameIdentifier => ClaimTypes.NameIdentifier;
    public static string NameUniqueIdentifier => ClaimTypes.Sid;
    public static string AccountTypeIdentifier => ClaimTypes.Upn;

    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
    }

    public static void AddName(this ICollection<Claim> claims, string name)
    {
        claims.Add(new Claim(ClaimTypes.Name, name));
    }

    public static void AddSurname(this ICollection<Claim> claims, string surname)
    {
        claims.Add(new Claim(ClaimTypes.Surname, surname));
    }

    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        claims.Add(new Claim(NameIdentifier, nameIdentifier));
    }

    public static void AddNameUniqueIdentifier(this ICollection<Claim> claims, string nameUniqueIdentifier)
    {
        claims.Add(new Claim(NameUniqueIdentifier, nameUniqueIdentifier));
    }

    public static void AddAccountTypeIdentifier(this ICollection<Claim> claims, string accountTypeIdentifier)
    {
        claims.Add(new Claim(AccountTypeIdentifier, accountTypeIdentifier));
    }

    public static void AddRoles(this ICollection<Claim> claims, string[] roles)
    {
        roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
    }
}