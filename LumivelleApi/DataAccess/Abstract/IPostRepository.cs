using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface IPostRepository : IDocumentDbRepository<PostDocument>
{
    Task<List<PostDocument>> GetFeedPageAsync(DateTime? cursor, int pageSize);
    Task<List<PostDocument>> GetByAccountIdPageAsync(ObjectId accountId, DateTime? cursor, int pageSize);
    Task<List<PostDocument>> GetByIdsAsync(IEnumerable<ObjectId> ids);
    Task<int> CountByAccountIdAsync(ObjectId accountId);
    Task<List<PostDocument>> GetActiveInWindowAsync(DateTime? since);
}