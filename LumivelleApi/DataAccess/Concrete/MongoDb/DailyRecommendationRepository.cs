using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class DailyRecommendationRepository
    : MongoDbRepositoryBase<DailyRecommendationDocument>, IDailyRecommendationRepository
{
    public DailyRecommendationRepository(MongoDbContext context)
        : base(context.MongoConnectionSettings)
    {
    }

    public async Task<DailyRecommendationDocument> GetByAccountAndDateAsync(ObjectId accountId, string localDate)
    {
        var filter = Builders<DailyRecommendationDocument>.Filter.Eq(x => x.AccountId, accountId)
            & Builders<DailyRecommendationDocument>.Filter.Eq(x => x.LocalDate, localDate)
            & Builders<DailyRecommendationDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
}
