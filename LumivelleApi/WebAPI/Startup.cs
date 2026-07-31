using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Autofac;
using Business;
using Business.DependencyResolvers;
using Business.Extensions;
using Business.Hub;
using Business.Middlewares;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using Core.Utilities.TaskScheduler.Hangfire;
using Core.Utilities.TaskScheduler.Hangfire.Models;
using Core.Utilities.Transformers;
using DataAccess.Abstract;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization; 
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Asp.Versioning; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using WebAPI.OpenApi;
using ConfigurationManager = Business.ConfigurationManager;

namespace WebAPI;

/// <summary>
///
/// </summary>
public partial class Startup : BusinessStartup
{
    /// <summary>
    /// Constructor of <c>Startup</c>
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="hostEnvironment"></param>
    public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        : base(configuration, hostEnvironment)
    {
    }


    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <remarks>
    /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
    /// </remarks>
    /// <param name="services"></param>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 524288000; // 500MB 
        });

        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddHttpContextAccessor();

        // Trust X-Forwarded-For / X-Forwarded-Proto only from explicitly configured
        // reverse proxies (e.g. nginx/ingress). With no proxies configured the
        // middleware is a no-op and the direct connection IP is used.
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownIPNetworks.Clear();
            options.KnownProxies.Clear();
            var knownProxies = Configuration.GetSection("ForwardedHeaders:KnownProxies").Get<string[]>() ?? [];
            foreach (var proxy in knownProxies)
                if (IPAddress.TryParse(proxy, out var ip))
                    options.KnownProxies.Add(ip);
        });

        services.AddApiVersioning(v =>
        {
            v.DefaultApiVersion = new ApiVersion(1, 0);
            v.AssumeDefaultVersionWhenUnspecified = true;
            v.ReportApiVersions = true;
            v.ApiVersionReader = new HeaderApiVersionReader("x-lumivelle-api-version");
        });

        services.AddSignalR().AddJsonProtocol(options =>
        {
            options.PayloadSerializerOptions.Converters.Add(
                new JsonStringEnumConverter());
        });

        // CORS: reflecting any origin together with AllowCredentials is a security
        // hole (any site could make credentialed cross-origin calls). Only allow
        // credentials for an explicit allow-list configured in "Cors:AllowedOrigins".
        // With no allow-list we fall back to any-origin WITHOUT credentials.
        var allowedOrigins = Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", builder =>
            {
                if (allowedOrigins.Length > 0)
                    builder.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                else
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                    ClockSkew = TimeSpan.Zero
                };


                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub/lumivelle-live"))
                            context.Token = accessToken;

                        return Task.CompletedTask;
                    }
                };
            });
        services.AddOpenApi(SwaggerMessages.Version, options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            options.AddOperationTransformer<AuthorizationOperationTransformer>();
        });

        // Rate limiting (built-in). A permissive global limiter protects the whole
        // API from floods; the stricter "auth" policy throttles credential / OTP
        // endpoints to slow brute-force attempts. Partitioned per client IP.
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1)
                    }));

            options.AddPolicy(RateLimitPolicies.Auth, httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1)
                    }));

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.Headers.RetryAfter = "60";
                await context.HttpContext.Response.WriteAsync(
                    "Too many requests. Please try again later.", token);
            };
        });


        base.ConfigureServices(services);
    }


    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // VERY IMPORTANT. Since we removed the build from AddDependencyResolvers, let's set the Service provider manually.
        // By the way, we can construct with DI by taking type to avoid calling static methods in aspects.
        ServiceTool.ServiceProvider = app.ApplicationServices;

        using (var seedScope = app.ApplicationServices.CreateScope())
        {
            var shadeRepository = seedScope.ServiceProvider.GetRequiredService<IShadeRepository>();
            var existingCount = shadeRepository.CountAsync(_ => true).GetAwaiter().GetResult();
            if (existingCount == 0)
            {
                foreach (var shade in Business.Handlers.Shades.ShadeSeedData.All())
                {
                    shadeRepository.AddAsync(shade).GetAwaiter().GetResult();
                }
            }

            var dailyEditPresetRepository =
                seedScope.ServiceProvider.GetRequiredService<IDailyEditPresetRepository>();
            var existingPresetCount = dailyEditPresetRepository.CountAsync(_ => true).GetAwaiter().GetResult();
            if (existingPresetCount == 0)
            {
                dailyEditPresetRepository
                    .AddManyAsync(Business.Handlers.Recommendations.DailyEditPresetSeedData.All())
                    .GetAwaiter().GetResult();
            }

            var languageRepository = seedScope.ServiceProvider.GetRequiredService<ILanguageRepository>();
            var existingLanguageCount = languageRepository.CountAsync(_ => true).GetAwaiter().GetResult();
            if (existingLanguageCount == 0)
            {
                languageRepository.AddManyAsync(Business.Handlers.Languages.LanguageSeedData.All())
                    .GetAwaiter().GetResult();
            }
        }

        var configurationManager = app.ApplicationServices.GetService<ConfigurationManager>();
        switch (configurationManager.Mode)
        {
            case ApplicationMode.Development:
                break;
            case ApplicationMode.Profiling:
            case ApplicationMode.Staging:
                break;
            case ApplicationMode.Production:
                break;
        }

        var httpContextAccessor = ServiceTool.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        LoggingServiceLocator.Initialize(httpContextAccessor);

        // Resolve the real client IP / scheme from proxy headers BEFORE any
        // middleware that depends on them (rate limiter, security block, logging).
        // Only proxies configured in "ForwardedHeaders:KnownProxies" are trusted,
        // so forwarded headers cannot be spoofed by arbitrary clients.
        app.UseForwardedHeaders();

        // Only expose detailed error pages in Development; the custom exception
        // middleware returns sanitized responses elsewhere.
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.ConfigureCustomExceptionMiddleware();
        app.UseMiddleware<SecurityBlockMiddleware>();

        app.UseCors("AllowOrigin");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseRateLimiter();

        app.UseAuthentication();

        app.UseAuthorization();

        // Make English your default language. It shouldn't change, according to the server.
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-US")
        });

        var cultureInfo = new CultureInfo("en-US")
        {
            DateTimeFormat =
            {
                ShortTimePattern = "HH:mm"
            }
        };

        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        var mediaPath = Path.Combine(Directory.GetCurrentDirectory(), "media");
        if (!Directory.Exists(mediaPath)) Directory.CreateDirectory(mediaPath);

        // Serve static files (incl. user-uploaded /media) with anti-sniffing
        // headers so the browser will not reinterpret/execute uploaded content
        // (defence-in-depth for stored-content XSS).
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                ctx.Context.Response.Headers["Content-Security-Policy"] = "default-src 'none'; sandbox";
            }
        });

        var taskSchedulerConfig = Configuration.GetSection("TaskSchedulerOptions").Get<TaskSchedulerConfig>();
        if (taskSchedulerConfig.Enabled)
        {
            app.UseHangfireDashboard(taskSchedulerConfig.Path, new DashboardOptions
            {
                DashboardTitle = taskSchedulerConfig.Title,
                Authorization =
                [
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = taskSchedulerConfig.Username,
                        Pass = taskSchedulerConfig.Password
                    }
                ]
            });

            AddRecurringJobs();
        }
 
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapOpenApi();
            endpoints.MapScalarApiReference();

            endpoints.MapControllers();
            endpoints.MapHub<TulparHub>("/hub/lumivelle-live");
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="builder"></param>
    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));
    }

    private void AddRecurringJobs()
    {
        var recurringJobService = ServiceTool.ServiceProvider.GetRequiredService<IRecurringJobService>();

        recurringJobService.AddOrUpdate<HangfireCleanupJob>(
            "cleanup-jobgraph",
            job => job.ExecuteAsync(),
            "0 3 * * *",
            TimeZoneInfo.Utc
        );
    }
}