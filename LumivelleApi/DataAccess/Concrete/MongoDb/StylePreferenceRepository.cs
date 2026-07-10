using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class StylePreferenceRepository
    : MongoDbRepositoryBase<StylePreferenceDocument>, IStylePreferenceRepository
{
    public StylePreferenceRepository(MongoDbContext context)
        : base(context.MongoConnectionSettings)
    {
    }

    public async Task<StylePreferenceDocument> GetByAccountIdAsync(ObjectId accountId)
    {
        return await _collection
            .Find(x => x.AccountId == accountId && x.Status == EntityStatus.Active)
            .FirstOrDefaultAsync();
    }
}
