using System;
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

public class PostRepository : MongoDbRepositoryBase<PostDocument>, IPostRepository
{
    public PostRepository(MongoDbContext context)
        : base(context.MongoConnectionSettings)
    {
    }

    public async Task<List<PostDocument>> GetFeedPageAsync(DateTime? cursor, int pageSize)
    {
        var filter = Builders<PostDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);
        if (cursor.HasValue)
            filter &= Builders<PostDocument>.Filter.Lt(x => x.CreatedAt, cursor.Value);

        return await _collection.Find(filter)
            .SortByDescending(x => x.CreatedAt)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task<List<PostDocument>> GetByAccountIdPageAsync(ObjectId accountId, DateTime? cursor, int pageSize)
    {
        var filter = Builders<PostDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<PostDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);
        if (cursor.HasValue)
            filter &= Builders<PostDocument>.Filter.Lt(x => x.CreatedAt, cursor.Value);

        return await _collection.Find(filter)
            .SortByDescending(x => x.CreatedAt)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task<List<PostDocument>> GetByIdsAsync(IEnumerable<ObjectId> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return [];

        var filter = Builders<PostDocument>.Filter.In(x => x.Id, idList)
            & Builders<PostDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<int> CountByAccountIdAsync(ObjectId accountId)
    {
        var filter = Builders<PostDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<PostDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return (int)await _collection.CountDocumentsAsync(filter);
    }
}
