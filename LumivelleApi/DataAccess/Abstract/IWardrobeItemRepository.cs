using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface IWardrobeItemRepository : IDocumentDbRepository<WardrobeItemDocument>
{
    Task<List<WardrobeItemDocument>> GetByAccountIdAsync(ObjectId accountId, string category);
    Task<List<WardrobeItemDocument>> GetByIdsAsync(IEnumerable<ObjectId> ids);
}