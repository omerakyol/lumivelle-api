using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface ISavedPostRepository : IDocumentDbRepository<SavedPostDocument>
{
    Task<SavedPostDocument> GetAsync(ObjectId postId, ObjectId accountId);
    Task<List<SavedPostDocument>> GetByAccountAndPostIdsAsync(ObjectId accountId, IEnumerable<ObjectId> postIds);
    Task<List<SavedPostDocument>> GetByAccountIdPageAsync(ObjectId accountId, DateTime? cursor, int pageSize);
    Task DeleteAllByPostIdAsync(ObjectId postId);

    Task<List<SavedPostDocument>> GetByAccountAndCollectionPageAsync(
        ObjectId accountId, ObjectId? collectionId, DateTime? cursor, int pageSize);

    Task<int> CountByAccountAndCollectionAsync(ObjectId accountId, ObjectId? collectionId);
    Task ClearCollectionIdAsync(ObjectId collectionId);
}