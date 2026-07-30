using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface IFollowRepository : IDocumentDbRepository<FollowDocument>
{
    Task<FollowDocument> GetAsync(ObjectId followerId, ObjectId followeeId);
    Task<int> CountFollowersAsync(ObjectId followeeId);
    Task<HashSet<ObjectId>> GetFollowedIdsAsync(ObjectId followerId, IEnumerable<ObjectId> candidateIds);
}