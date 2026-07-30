using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class DailyRecommendationRepository(MongoDbContext context)
    : MongoDbRepositoryBase<DailyRecommendationDocument>(context.MongoConnectionSettings),
        IDailyRecommendationRepository
{
    public async Task<DailyRecommendationDocument> GetByAccountAndDateAsync(ObjectId accountId, string localDate)
    {
        var filter = Builders<DailyRecommendationDocument>.Filter.Eq(x => x.AccountId, accountId)
                     & Builders<DailyRecommendationDocument>.Filter.Eq(x => x.LocalDate, localDate)
                     & Builders<DailyRecommendationDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
}