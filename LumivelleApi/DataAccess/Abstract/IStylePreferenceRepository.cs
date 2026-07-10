using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface IStylePreferenceRepository : IDocumentDbRepository<StylePreferenceDocument>
{
    Task<StylePreferenceDocument> GetByAccountIdAsync(ObjectId accountId);
}
