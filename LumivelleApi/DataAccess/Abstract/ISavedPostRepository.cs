using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface ISavedPostRepository : IDocumentDbRepository<SavedPostDocument>
{
    Task<SavedPostDocument> GetAsync(ObjectId postId, ObjectId accountId);
    Task<List<SavedPostDocument>> GetByAccountAndPostIdsAsync(ObjectId accountId, IEnumerable<ObjectId> postIds);
    Task<List<SavedPostDocument>> GetByAccountIdPageAsync(ObjectId accountId, System.DateTime? cursor, int pageSize);
    Task DeleteAllByPostIdAsync(ObjectId postId);
}
