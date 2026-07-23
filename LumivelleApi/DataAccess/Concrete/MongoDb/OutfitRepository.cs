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

public class OutfitRepository : MongoDbRepositoryBase<OutfitDocument>, IOutfitRepository
{
    public OutfitRepository(MongoDbContext context)
        : base(context.MongoConnectionSettings)
    {
    }

    public async Task<List<OutfitDocument>> GetByAccountIdAsync(ObjectId accountId)
    {
        var filter = Builders<OutfitDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<OutfitDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).SortByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<int> CountByItemIdAsync(ObjectId accountId, ObjectId itemId)
    {
        var filter = Builders<OutfitDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<OutfitDocument>.Filter.Eq(x => x.Status, EntityStatus.Active)
            & Builders<OutfitDocument>.Filter.AnyEq(x => x.ItemIds, itemId);

        return (int)await _collection.CountDocumentsAsync(filter);
    }

    public async Task<List<OutfitDocument>> GetByIdsAsync(IEnumerable<ObjectId> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return [];

        var filter = Builders<OutfitDocument>.Filter.In(x => x.Id, idList)
            & Builders<OutfitDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).ToListAsync();
    }
}
