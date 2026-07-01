using System.Diagnostics;
using System.Reflection;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Utilities.IoC;
using Core.Utilities.Mail;
using Core.Utilities.Uri;
using Core.Utilities.URI;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers;

public class CoreModule : ICoreModule
{
    public void Load(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheManager, MemoryCacheManager>();
        services.AddSingleton<IMailService, MailManager>();
        services.AddSingleton<IEmailConfiguration, EmailConfiguration>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<Stopwatch>();
        var serviceCollection = services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.AddSingleton<IUriService>(o =>
        {
            var accessor = o.GetRequiredService<IHttpContextAccessor>();
            var request = accessor.HttpContext?.Request;
            var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent(), request?.PathBase);
            return new UriManager(uri);
        });
    }
}