using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface IPostLikeRepository : IDocumentDbRepository<PostLikeDocument>
{
    Task<PostLikeDocument> GetAsync(ObjectId postId, ObjectId accountId);
    Task<List<PostLikeDocument>> GetByAccountAndPostIdsAsync(ObjectId accountId, IEnumerable<ObjectId> postIds);
    Task DeleteAllByPostIdAsync(ObjectId postId);
}