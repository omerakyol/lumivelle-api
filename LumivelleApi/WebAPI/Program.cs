using System;
using Autofac.Extensions.DependencyInjection;
using Business.Hub;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAPI;

/// <summary>
///
/// </summary>
public static class Program
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseKestrel(options =>
                    {
                        // Max upload size (example: 500 MB)
                        options.Limits.MaxRequestBodySize = 500 * 1024 * 1024;

                        // Prevent slow uploads from timing out
                        options.Limits.MinRequestBodyDataRate = null;

                        // Increase timeouts
                        options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
                        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
                    })
                    .UseStartup<Startup>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            });
    }
}