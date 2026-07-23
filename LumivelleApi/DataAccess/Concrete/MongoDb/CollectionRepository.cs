using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class CollectionRepository : MongoDbRepositoryBase<CollectionDocument>, ICollectionRepository
{
    public CollectionRepository(MongoDbContext context)
        : base(context.MongoConnectionSettings)
    {
    }

    public async Task<List<CollectionDocument>> GetByAccountIdAsync(ObjectId accountId)
    {
        var filter = Builders<CollectionDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<CollectionDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).SortByDescending(x => x.CreatedAt).ToListAsync();
    }
}
