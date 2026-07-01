using System;
using Core.Constants;
using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Serilog;
using Serilog.Formatting.Compact;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class MongoDbLogger : LoggerServiceBase
{
    public MongoDbLogger()
    {
        var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        var logConfig = configuration.GetSection("SeriLogConfigurations:MongoDbConfiguration")
            .Get<MongoDbConfiguration>();

        var connectionStringWithDatabase = BuildConnectionString(
            logConfig.ConnectionString,
            logConfig.DatabaseName);

        Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.With<MongoDbExceptionEnricher>()
            .Enrich.With<UserContextEnricher>()
            .Enrich.With<PerformanceEnricher>() 
            .Enrich.WithProperty("Application", GlobalConfig.ApplicationName)
            .Enrich.WithProperty("Environment", GetEnvironment())
            .WriteTo.MongoDB(
                databaseUrl: connectionStringWithDatabase,
                collectionName: logConfig.Collection,
                mongoDBJsonFormatter: new CompactJsonFormatter(),
                batchPostingLimit: 100,
                period: TimeSpan.FromSeconds(5))
            .CreateLogger();
    }

    /// <summary>
    /// Builds a MongoDB connection string by combining base connection string with database name
    /// </summary>
    /// <param name="connectionString">Base MongoDB connection string</param>
    /// <param name="databaseName">Database name to append</param>
    /// <returns>Complete connection string with database name</returns>
    private static string BuildConnectionString(string connectionString, string databaseName)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));

        if (string.IsNullOrWhiteSpace(databaseName))
            throw new ArgumentException("Database name cannot be null or empty", nameof(databaseName));

        var builder = new MongoUrlBuilder(connectionString)
        {
            DatabaseName = databaseName
        };

        return builder.ToString();
    }

    private static string GetEnvironment()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    }
}