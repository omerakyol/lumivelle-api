using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class WardrobeItemRepository
    : MongoDbRepositoryBase<WardrobeItemDocument>, IWardrobeItemRepository
{
    public WardrobeItemRepository(MongoDbContext context)
        : base(context.MongoConnectionSettings)
    {
    }

    public async Task<List<WardrobeItemDocument>> GetByAccountIdAsync(ObjectId accountId, string category)
    {
        var filter = Builders<WardrobeItemDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<WardrobeItemDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        if (!string.IsNullOrEmpty(category))
            filter &= Builders<WardrobeItemDocument>.Filter.Eq(x => x.Category, category);

        return await _collection.Find(filter).SortByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<List<WardrobeItemDocument>> GetByIdsAsync(IEnumerable<ObjectId> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return [];

        var filter = Builders<WardrobeItemDocument>.Filter.In(x => x.Id, idList)
            & Builders<WardrobeItemDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).ToListAsync();
    }
}
