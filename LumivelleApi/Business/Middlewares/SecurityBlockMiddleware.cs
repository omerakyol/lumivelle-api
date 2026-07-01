using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; 

namespace Business.Middlewares;

public sealed class SecurityBlockMiddleware(RequestDelegate next)
{
    private static readonly string[] BlockedUserAgents =
    {
        "python-requests",
        "curl",
        "wget",
        "httpclient",
        "postmanruntime"
    };

    private static readonly HashSet<string> BlockedIps = new()
    {
    };

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString();
        var userAgent = context.Request.Headers.UserAgent.ToString().ToLowerInvariant();

        if (
            (
                !string.IsNullOrEmpty(ip) && BlockedIps.Contains(ip)
            ) ||
            BlockedUserAgents.Any(x => userAgent.Contains(x)))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        await next(context);
    }
}