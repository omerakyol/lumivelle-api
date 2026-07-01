using Core.DataAccess.MongoDb.Concrete.Configurations;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.MongoDb.Context;

public abstract class MongoDbContextBase(IConfiguration configuration)
{
    public MongoConnectionSettings MongoConnectionSettings { get; } =
        configuration.GetSection("MongoDbSettings").Get<MongoConnectionSettings>();
}