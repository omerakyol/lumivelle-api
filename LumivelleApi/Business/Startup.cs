using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using Anthropic.SDK;
using Business.Helpers;
using Business.Services.AiServices;
using Core.Constants;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.DataAccess.MongoDb.Concrete.Configurations;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.ElasticSearch;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.Jwt;
using Core.Utilities.TaskScheduler.Hangfire;
using Core.Utilities.TaskScheduler.Hangfire.Models;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.MongoDb;
using DataAccess.Concrete.MongoDb.Context;
using FluentValidation;
using Hangfire;
using Hangfire.InMemory;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.PostgreSql;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization.Conventions;
using OpenAI;

namespace Business;

public class BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
{
    protected IConfiguration Configuration { get; } = configuration;

    protected IHostEnvironment HostEnvironment { get; } = hostEnvironment;

    /// <summary>
    ///     This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <remarks>
    ///     It is common to all configurations and must be called. Aspnet core does not call this method because there are
    ///     other methods.
    /// </remarks>
    /// <param name="services"></param>
    public virtual void ConfigureServices(IServiceCollection services)
    {
        var conventionPack = new ConventionPack
        {
            new CamelCaseElementNameConvention()
        };

        ConventionRegistry.Register("CamelCase", conventionPack, t => true);

        ClaimsPrincipal GetPrincipal(IServiceProvider sp)
        {
            return sp.GetService<IHttpContextAccessor>()?.HttpContext?.User ??
                   new ClaimsPrincipal(new ClaimsIdentity(Messages.Unknown));
        }

        services.AddScoped<IPrincipal>(GetPrincipal);
        services.AddMemoryCache();

        services.AddScoped<ISignalRClientHelper, SignalRClientHelper>();

        var coreModule = new CoreModule();
        services.AddDependencyResolvers(Configuration, [coreModule]);

        services.AddSingleton<ConfigurationManager>();

        services.AddTransient<MongoDbLogger>();
        services.AddScoped<IpControlAttribute>();

        services.AddTransient<ITokenHelper, JwtHelper>();
        services.AddTransient<IElasticSearch, ElasticSearchManager>();

        services.AddScoped<ILanguageHelper, LanguageHelper>();

        services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
        services.AddTransient<IMessageConsumer, MqConsumerHelper>();

        services.AddSingleton<MongoDbContext>();

        services.AddScoped<IFirebaseNotificationService, FirebaseNotificationService>();
        services.AddScoped<IExpoNotificationService, ExpoNotificationService>();
        services.AddScoped<IApplePurchaseValidator, ApplePurchaseValidator>();
        services.AddScoped<IGooglePlayValidator, GooglePlayValidator>();
        services.AddScoped<IGoogleSignInVerifier, GoogleSignInVerifier>();
        services.AddScoped<IAppleSignInVerifier, AppleSignInVerifier>();

        services.AddScoped<ILogRepository, LogRepository>();
        services.AddScoped<ITranslateRepository, TranslateRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IBeautyProfileRepository, BeautyProfileRepository>();
        services.AddScoped<IWardrobeItemRepository, WardrobeItemRepository>();
        services.AddScoped<IOutfitRepository, OutfitRepository>();
        services.AddScoped<IDailyRecommendationRepository, DailyRecommendationRepository>();
        services.AddScoped<IDailyEditPresetRepository, DailyEditPresetRepository>();
        services.AddScoped<IStylePreferenceRepository, StylePreferenceRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPostLikeRepository, PostLikeRepository>();
        services.AddScoped<ISavedPostRepository, SavedPostRepository>();
        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<IMediaFileRepository, MediaFileRepository>();
        services.AddScoped<IShadeRepository, ShadeRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();

        var firebaseServiceAccountPath = Path.Combine(
            ((IWebHostEnvironment)HostEnvironment).WebRootPath, "firebase-config.json");

        var aiApiKeys = RemoteConfigApiKeyProvider
            .GetParametersAsync(firebaseServiceAccountPath, "ClaudeApiKey", "OpenAiApiKey")
            .GetAwaiter().GetResult();

        services.AddSingleton(_ => new OpenAIClient(aiApiKeys["OpenAiApiKey"]));

        services.AddSingleton(_ => new AnthropicClient(aiApiKeys["ClaudeApiKey"]));

        services.AddTransient<OpenAiService>();
        services.AddTransient<ClaudeService>();
        services.AddTransient<IAiServiceFactory, AiServiceFactory>();

        var taskSchedulerConfig = Configuration.GetSection("TaskSchedulerOptions").Get<TaskSchedulerConfig>();
        if (taskSchedulerConfig.Enabled)
        {
            services.AddTransient<IJobService, HangfireJobService>();
            services.AddTransient<IRecurringJobService, HangfireRecurringJobService>();

            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
                config.UseSimpleAssemblyNameTypeSerializer();
                config.UseRecommendedSerializerSettings();

                config.UseSerilogLogProvider();

                switch (taskSchedulerConfig.StorageType.ToLower())
                {
                    case "mongodb":

                        var mongoSettings = Configuration.GetSection("MongoDbSettings").Get<MongoConnectionSettings>();
                        config.UseMongoStorage(mongoSettings.ConnectionString, mongoSettings.DatabaseName,
                            new MongoStorageOptions
                            {
                                MigrationOptions = new MongoMigrationOptions
                                {
                                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                                    BackupStrategy = new CollectionMongoBackupStrategy()
                                },
                                Prefix = "hangfire",
                                CheckConnection = true,
                                JobExpirationCheckInterval = TimeSpan.FromHours(1),
                                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5)
                            });

                        break;
                    case "postgresql":
                        var postgreSqlStorageOptions = new PostgreSqlStorageOptions
                        {
                            PrepareSchemaIfNecessary = true
                        };
                        config.UsePostgreSqlStorage(
                            configure => configure.UseNpgsqlConnection(taskSchedulerConfig.ConnectionString),
                            postgreSqlStorageOptions);
                        break;
                    case "mssql":
                        var sqlServerStorageOptions = new SqlServerStorageOptions
                        {
                            PrepareSchemaIfNecessary = true
                        };
                        config.UseSqlServerStorage(taskSchedulerConfig.ConnectionString,
                            sqlServerStorageOptions);
                        break;
                    case "inmemory":
                        var inMemoryOptions = new InMemoryStorageOptions();
                        config.UseInMemoryStorage(inMemoryOptions);
                        break;
                }
            });

            services.AddHangfireServer();
        }

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(BusinessStartup).Assembly); });

        ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) => memberInfo
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName();
    }
}