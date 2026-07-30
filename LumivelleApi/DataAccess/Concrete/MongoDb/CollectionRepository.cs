using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class CollectionRepository(MongoDbContext context)
    : MongoDbRepositoryBase<CollectionDocument>(context.MongoConnectionSettings), ICollectionRepository
{
    public async Task<List<CollectionDocument>> GetByAccountIdAsync(ObjectId accountId)
    {
        var filter = Builders<CollectionDocument>.Filter.Eq(x => x.AccountId, accountId)
                     & Builders<CollectionDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).SortByDescending(x => x.CreatedAt).ToListAsync();
    }
}