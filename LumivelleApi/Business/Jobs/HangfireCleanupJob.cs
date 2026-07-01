using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete.Configurations;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class HangfireCleanupJob
{
    private readonly string _connectionString;
    private readonly string _databaseName;

    public HangfireCleanupJob()
    {
        var configuration = ServiceTool.ServiceProvider.GetRequiredService<IConfiguration>();
        var mongoConnectionSettings = configuration.GetSection("MongoDbSettings").Get<MongoConnectionSettings>();
        _connectionString = mongoConnectionSettings.ConnectionString;
        _databaseName = mongoConnectionSettings.DatabaseName;
    }

    /// <summary>
    /// Cleanup old jobGraph collection data
    /// </summary>
    public async Task ExecuteAsync()
    {
        var client = new MongoClient(_connectionString);
        var database = client.GetDatabase(_databaseName);

        var olderThanDays = 1;
        var collection = database.GetCollection<MongoDB.Bson.BsonDocument>("hangfire.jobGraph");
        var cutoff = DateTime.UtcNow.AddDays(-olderThanDays);

        var filter = Builders<MongoDB.Bson.BsonDocument>.Filter.Or(
            Builders<MongoDB.Bson.BsonDocument>.Filter.Lt("ExpireAt", cutoff),
            Builders<MongoDB.Bson.BsonDocument>.Filter.Lt("CreatedAt", cutoff),
            Builders<MongoDB.Bson.BsonDocument>.Filter.Eq("StateName", "Succeeded"));

        var result = await collection.DeleteManyAsync(filter);
        Console.WriteLine($"[HangfireCleanup] {result.DeletedCount} cleaned up jobs.");
    }
}