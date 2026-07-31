using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class DailyEditPresetRepository(MongoDbContext context)
    : MongoDbRepositoryBase<DailyEditPresetDocument>(context.MongoConnectionSettings),
        IDailyEditPresetRepository
{
    public async Task<List<DailyEditPresetDocument>> GetByBucketAsync(
        string seasonFamily, string undertone, string contrast)
    {
        var filter = Builders<DailyEditPresetDocument>.Filter.Eq(x => x.SeasonFamily, seasonFamily)
                     & Builders<DailyEditPresetDocument>.Filter.Eq(x => x.Undertone, undertone)
                     & Builders<DailyEditPresetDocument>.Filter.Eq(x => x.Contrast, contrast)
                     & Builders<DailyEditPresetDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);

        return await _collection.Find(filter).SortBy(x => x.SortOrder).ToListAsync();
    }
}
