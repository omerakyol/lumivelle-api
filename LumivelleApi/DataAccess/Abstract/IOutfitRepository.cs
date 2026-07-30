using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface IOutfitRepository : IDocumentDbRepository<OutfitDocument>
{
    Task<List<OutfitDocument>> GetByAccountIdAsync(ObjectId accountId);
    Task<int> CountByItemIdAsync(ObjectId accountId, ObjectId itemId);
    Task<List<OutfitDocument>> GetByIdsAsync(IEnumerable<ObjectId> ids);
}