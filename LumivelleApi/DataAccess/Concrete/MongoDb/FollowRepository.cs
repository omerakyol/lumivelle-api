using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class FollowRepository : MongoDbRepositoryBase<FollowDocument>, IFollowRepository
{
    public FollowRepository(MongoDbContext context)
        : base(context.MongoConnectionSettings)
    {
        CreateIndexes();
    }

    public async Task<FollowDocument> GetAsync(ObjectId followerId, ObjectId followeeId)
    {
        var filter = Builders<FollowDocument>.Filter.Eq(x => x.FollowerId, followerId)
                     & Builders<FollowDocument>.Filter.Eq(x => x.FolloweeId, followeeId)
                     & Builders<FollowDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<int> CountFollowersAsync(ObjectId followeeId)
    {
        var filter = Builders<FollowDocument>.Filter.Eq(x => x.FolloweeId, followeeId)
                     & Builders<FollowDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return (int)await _collection.CountDocumentsAsync(filter);
    }

    public async Task<HashSet<ObjectId>> GetFollowedIdsAsync(ObjectId followerId, IEnumerable<ObjectId> candidateIds)
    {
        var idList = candidateIds.Distinct().ToList();
        if (idList.Count == 0)
            return [];

        var filter = Builders<FollowDocument>.Filter.Eq(x => x.FollowerId, followerId)
                     & Builders<FollowDocument>.Filter.In(x => x.FolloweeId, idList)
                     & Builders<FollowDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        var docs = await _collection.Find(filter).ToListAsync();
        return docs.Select(d => d.FolloweeId).ToHashSet();
    }

    private void CreateIndexes()
    {
        var uniquePairKeys = Builders<FollowDocument>.IndexKeys
            .Ascending(x => x.FollowerId)
            .Ascending(x => x.FolloweeId);

        _collection.Indexes.CreateOne(
            new CreateIndexModel<FollowDocument>(uniquePairKeys, new CreateIndexOptions { Unique = true }));

        var followeeKeys = Builders<FollowDocument>.IndexKeys.Ascending(x => x.FolloweeId);
        _collection.Indexes.CreateOne(new CreateIndexModel<FollowDocument>(followeeKeys));
    }
}