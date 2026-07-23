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

public class SavedPostRepository : MongoDbRepositoryBase<SavedPostDocument>, ISavedPostRepository
{
    public SavedPostRepository(MongoDbContext context)
        : base(context.MongoConnectionSettings)
    {
        CreateIndexes();
    }

    private void CreateIndexes()
    {
        var indexKeys = Builders<SavedPostDocument>.IndexKeys
            .Ascending(x => x.PostId)
            .Ascending(x => x.AccountId);

        _collection.Indexes.CreateOne(
            new CreateIndexModel<SavedPostDocument>(indexKeys, new CreateIndexOptions { Unique = true }));
    }

    public async Task<SavedPostDocument> GetAsync(ObjectId postId, ObjectId accountId)
    {
        var filter = Builders<SavedPostDocument>.Filter.Eq(x => x.PostId, postId)
            & Builders<SavedPostDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<SavedPostDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<SavedPostDocument>> GetByAccountAndPostIdsAsync(
        ObjectId accountId, IEnumerable<ObjectId> postIds)
    {
        var idList = postIds.ToList();
        if (idList.Count == 0)
            return [];

        var filter = Builders<SavedPostDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<SavedPostDocument>.Filter.In(x => x.PostId, idList)
            & Builders<SavedPostDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<List<SavedPostDocument>> GetByAccountIdPageAsync(
        ObjectId accountId, DateTime? cursor, int pageSize)
    {
        var filter = Builders<SavedPostDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<SavedPostDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);
        if (cursor.HasValue)
            filter &= Builders<SavedPostDocument>.Filter.Lt(x => x.CreatedAt, cursor.Value);

        return await _collection.Find(filter)
            .SortByDescending(x => x.CreatedAt)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task DeleteAllByPostIdAsync(ObjectId postId)
    {
        var filter = Builders<SavedPostDocument>.Filter.Eq(x => x.PostId, postId);
        await _collection.DeleteManyAsync(filter);
    }

    public async Task<List<SavedPostDocument>> GetByAccountAndCollectionPageAsync(
        ObjectId accountId, ObjectId? collectionId, DateTime? cursor, int pageSize)
    {
        var filter = Builders<SavedPostDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<SavedPostDocument>.Filter.Eq(x => x.Status, EntityStatus.Active)
            & Builders<SavedPostDocument>.Filter.Eq(x => x.CollectionId, collectionId);
        if (cursor.HasValue)
            filter &= Builders<SavedPostDocument>.Filter.Lt(x => x.CreatedAt, cursor.Value);

        return await _collection.Find(filter)
            .SortByDescending(x => x.CreatedAt)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountByAccountAndCollectionAsync(ObjectId accountId, ObjectId? collectionId)
    {
        var filter = Builders<SavedPostDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<SavedPostDocument>.Filter.Eq(x => x.Status, EntityStatus.Active)
            & Builders<SavedPostDocument>.Filter.Eq(x => x.CollectionId, collectionId);

        return (int)await _collection.CountDocumentsAsync(filter);
    }

    public async Task ClearCollectionIdAsync(ObjectId collectionId)
    {
        var filter = Builders<SavedPostDocument>.Filter.Eq(x => x.CollectionId, collectionId);
        var update = Builders<SavedPostDocument>.Update.Set(x => x.CollectionId, null);
        await _collection.UpdateManyAsync(filter, update);
    }
}
