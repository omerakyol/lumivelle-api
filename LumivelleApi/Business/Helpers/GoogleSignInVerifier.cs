using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace Business.Helpers;

public record GoogleIdentity(string Sub, string Email, bool EmailVerified);

public interface IGoogleSignInVerifier
{
    /// <summary>
    /// Verifies a Google ID token's signature, issuer, audience, and expiry against Google's
    /// public keys, returning the verified identity, or null if the token is invalid.
    /// </summary>
    Task<GoogleIdentity> VerifyAsync(string idToken);
}

public class GoogleSignInVerifier(IConfiguration configuration) : IGoogleSignInVerifier
{
    // iOS, Android, and web OAuth client ids that are allowed as a token audience — see
    // GoogleAuthSettings:ClientIds in appsettings.json.
    private readonly string[] _clientIds = configuration.GetSection("GoogleAuthSettings:ClientIds").Get<string[]>() ?? [];

    public async Task<GoogleIdentity> VerifyAsync(string idToken)
    {
        if (string.IsNullOrWhiteSpace(idToken))
            return null;

        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = _clientIds
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            return new GoogleIdentity(payload.Subject, payload.Email, payload.EmailVerified);
        }
        catch (InvalidJwtException)
        {
            return null;
        }
    }
}
