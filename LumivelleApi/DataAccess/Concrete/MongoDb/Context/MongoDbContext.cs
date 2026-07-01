using Core.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb.Context;

public class MongoDbContext : MongoDbContextBase
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
        : base(configuration)
    {
        var client = new MongoClient(MongoConnectionSettings.ConnectionString);
        _database = client.GetDatabase(MongoConnectionSettings.DatabaseName);
    }

    public IMongoCollection<Account> Accounts => _database.GetCollection<Account>("accounts");
    public IMongoCollection<RefreshToken> RefreshTokens => _database.GetCollection<RefreshToken>("refreshTokens");
    public IMongoCollection<Language> Languages => _database.GetCollection<Language>("languages");
    public IMongoCollection<Translate> Translates => _database.GetCollection<Translate>("translates");
    public IMongoCollection<Log> Logs => _database.GetCollection<Log>("logs");
}