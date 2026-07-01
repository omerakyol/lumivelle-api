using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Driver;

namespace DataAccess.Concrete.EntityFramework;

public class AccountRepository
    : MongoDbRepositoryBase<Account>, IAccountRepository
{
    private readonly MongoDbContext _context;

    public AccountRepository(MongoDbContext context) : base(
        context.MongoConnectionSettings)
    {
        _context = context;
        CreateIndexes();
    }

    private void CreateIndexes()
    {
        var indexKeys = Builders<Account>.IndexKeys
            .Ascending(i => i.CreatedAt)
            .Ascending(i => i.Status);

        _context.Accounts.Indexes.CreateOne(new CreateIndexModel<Account>(indexKeys));
    }
}