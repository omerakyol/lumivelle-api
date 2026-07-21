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

public class PostLikeRepository : MongoDbRepositoryBase<PostLikeDocument>, IPostLikeRepository
{
    public PostLikeRepository(MongoDbContext context)
        : base(context.MongoConnectionSettings)
    {
        CreateIndexes();
    }

    private void CreateIndexes()
    {
        var indexKeys = Builders<PostLikeDocument>.IndexKeys
            .Ascending(x => x.PostId)
            .Ascending(x => x.AccountId);

        _collection.Indexes.CreateOne(
            new CreateIndexModel<PostLikeDocument>(indexKeys, new CreateIndexOptions { Unique = true }));
    }

    public async Task<PostLikeDocument> GetAsync(ObjectId postId, ObjectId accountId)
    {
        var filter = Builders<PostLikeDocument>.Filter.Eq(x => x.PostId, postId)
            & Builders<PostLikeDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<PostLikeDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<PostLikeDocument>> GetByAccountAndPostIdsAsync(
        ObjectId accountId, IEnumerable<ObjectId> postIds)
    {
        var idList = postIds.ToList();
        if (idList.Count == 0)
            return [];

        var filter = Builders<PostLikeDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<PostLikeDocument>.Filter.In(x => x.PostId, idList)
            & Builders<PostLikeDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).ToListAsync();
    }

    public async Task DeleteAllByPostIdAsync(ObjectId postId)
    {
        var filter = Builders<PostLikeDocument>.Filter.Eq(x => x.PostId, postId);
        await _collection.DeleteManyAsync(filter);
    }
}
