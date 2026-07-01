namespace WebAPI;

/// <summary>
/// Named rate-limiting policies applied to controller actions via
/// <c>[EnableRateLimiting(...)]</c>.
/// </summary>
public static class RateLimitPolicies
{
    /// <summary>
    /// Strict per-IP limit for authentication / OTP endpoints (login, register,
    /// password reset, two-factor) to slow brute-force and abuse.
    /// </summary>
    public const string Auth = "auth";
}
