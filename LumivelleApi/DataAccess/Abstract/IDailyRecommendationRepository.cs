using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface IDailyRecommendationRepository : IDocumentDbRepository<DailyRecommendationDocument>
{
    Task<DailyRecommendationDocument> GetByAccountAndDateAsync(ObjectId accountId, string localDate, string language);
}