using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface ICommentRepository : IDocumentDbRepository<CommentDocument>
{
    Task<List<CommentDocument>> GetByPostIdPageAsync(ObjectId postId, System.DateTime? cursor, int pageSize);
    Task DeleteAllByPostIdAsync(ObjectId postId);
}
