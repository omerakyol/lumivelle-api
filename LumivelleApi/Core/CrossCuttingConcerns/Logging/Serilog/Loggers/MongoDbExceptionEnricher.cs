using System;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class MongoDbExceptionEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Exception == null)
            return;

        var exception = logEvent.Exception;

        logEvent.AddPropertyIfAbsent(
            propertyFactory.CreateProperty("ExceptionType", exception.GetType().Name));

        logEvent.AddPropertyIfAbsent(
            propertyFactory.CreateProperty("ExceptionMessage", exception.Message));

        logEvent.AddPropertyIfAbsent(
            propertyFactory.CreateProperty("StackTrace", exception.StackTrace));

        if (exception.InnerException == null) return;

        if (exception.InnerException.InnerException != null)
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("InnerExceptionType",
                    exception.InnerException.InnerException.GetType().Name));

            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("InnerExceptionMessage",
                    exception.InnerException.InnerException.Message));
        }
        else
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("InnerExceptionType", exception.InnerException.GetType().Name));

            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("InnerExceptionMessage", exception.InnerException.Message));
        }
    }
}

/// <summary>
/// Enriches logs with user and request information
/// </summary>
public class UserContextEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var httpContextAccessor = LoggingServiceLocator.HttpContextAccessor;
        var httpContext = httpContextAccessor?.HttpContext;
        if (httpContext == null)
            return;

        // User ID
        var userId = httpContext.User?.FindFirst("sub")?.Value ??
                     httpContext.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                         ?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("UserId", userId));
        }

        // Email
        var email = httpContext.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(email))
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("Email", email));
        }

        // IP Address 
        var ipAddress = httpContext.Connection?.RemoteIpAddress?.ToString();
        ipAddress = ipAddress?.Replace("::ffff:", "", StringComparison.OrdinalIgnoreCase);

        if (string.IsNullOrEmpty(ipAddress) || ipAddress == "172.17.0.1")
        {
            if (httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                ipAddress = forwardedFor.ToString().Split(',')[0].Trim();
            }

            else if (httpContext.Request.Headers.TryGetValue("X-Real-IP", out var realIp))
            {
                ipAddress = realIp.ToString();
            }

            else if (httpContext.Request.Headers.TryGetValue("CF-Connecting-IP", out var cfIp))
            {
                ipAddress = cfIp.ToString();
            }
        }

        if (!string.IsNullOrEmpty(ipAddress))
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("IpAddress", ipAddress));
        }

        // User Agent
        if (httpContext.Request.Headers.TryGetValue("User-Agent", out var userAgent))
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("UserAgent", userAgent.ToString()));
        }
    }
}

/// <summary>
/// Enriches logs with performance metrics
/// </summary>
public class PerformanceEnricher : ILogEventEnricher
{
    private const string RequestStartTimeKey = "RequestStartTime";

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var httpContextAccessor = LoggingServiceLocator.HttpContextAccessor;
        var httpContext = httpContextAccessor?.HttpContext;
        if (httpContext == null)
            return;

        // Request duration
        if (httpContext.Items.TryGetValue(RequestStartTimeKey, out var startTime) &&
            startTime is DateTime startDateTime)
        {
            var duration = DateTime.UtcNow - startDateTime;
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("RequestDurationMs", duration.TotalMilliseconds));
        }

        // Response status code
        logEvent.AddPropertyIfAbsent(
            propertyFactory.CreateProperty("StatusCode", httpContext.Response.StatusCode));
    }

    public static void SetRequestStartTime(HttpContext httpContext)
    {
        httpContext.Items["RequestStartTime"] = DateTime.UtcNow;
    }
}