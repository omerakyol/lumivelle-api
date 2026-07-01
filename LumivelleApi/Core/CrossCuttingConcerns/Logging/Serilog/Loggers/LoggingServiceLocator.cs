using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

/// <summary>
/// Service locator for logging enrichers to access HttpContextAccessor
/// </summary>
public static class LoggingServiceLocator
{
    private static IHttpContextAccessor _httpContextAccessor;

    public static IHttpContextAccessor HttpContextAccessor => _httpContextAccessor;

    public static void Initialize(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
}