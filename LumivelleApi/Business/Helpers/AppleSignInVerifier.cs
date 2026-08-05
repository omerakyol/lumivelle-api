using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Helpers;

public record AppleIdentity(string Sub, string Email, bool EmailVerified);

public interface IAppleSignInVerifier
{
    /// <summary>
    /// Verifies an Apple identity token's signature (against Apple's published JWKS), issuer,
    /// audience, and expiry, returning the verified identity, or null if the token is invalid.
    /// </summary>
    Task<AppleIdentity> VerifyAsync(string identityToken);
}

public class AppleSignInVerifier(IConfiguration configuration) : IAppleSignInVerifier
{
    private const string Issuer = "https://appleid.apple.com";
    private const string JwksUrl = "https://appleid.apple.com/auth/keys";

    // Bundle id (native sign-in) or Services ID (web flow) — see AppleAuthSettings:ClientId in
    // appsettings.json. Comma-separated to allow both without extra config plumbing.
    private readonly string[] _audiences =
        (configuration["AppleAuthSettings:ClientId"] ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    private static readonly HttpClient HttpClient = new();
    private static JsonWebKeySet _cachedKeySet;
    private static DateTime _cachedAt = DateTime.MinValue;

    public async Task<AppleIdentity> VerifyAsync(string identityToken)
    {
        if (string.IsNullOrWhiteSpace(identityToken))
            return null;

        try
        {
            var keySet = await GetJwksAsync();
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidIssuer = Issuer,
                ValidAudiences = _audiences,
                IssuerSigningKeys = keySet.GetSigningKeys(),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            };

            var principal = handler.ValidateToken(identityToken, parameters, out _);
            var sub = principal.FindFirst("sub")?.Value;
            var email = principal.FindFirst("email")?.Value;
            var emailVerifiedClaim = principal.FindFirst("email_verified")?.Value;
            var emailVerified = string.Equals(emailVerifiedClaim, "true", StringComparison.OrdinalIgnoreCase);

            return string.IsNullOrEmpty(sub) ? null : new AppleIdentity(sub, email, emailVerified);
        }
        catch (Exception)
        {
            // Covers SecurityTokenException (signature/issuer/audience/expiry failures) and any
            // JWKS-fetch failure — either way the token can't be trusted.
            return null;
        }
    }

    // Apple's signing keys rotate infrequently; a short in-memory cache avoids fetching the JWKS
    // on every social-login request without needing a distributed cache for this single value.
    private static async Task<JsonWebKeySet> GetJwksAsync()
    {
        if (_cachedKeySet != null && DateTime.UtcNow - _cachedAt < TimeSpan.FromHours(6))
            return _cachedKeySet;

        var json = await HttpClient.GetStringAsync(JwksUrl);
        _cachedKeySet = new JsonWebKeySet(json);
        _cachedAt = DateTime.UtcNow;
        return _cachedKeySet;
    }
}
