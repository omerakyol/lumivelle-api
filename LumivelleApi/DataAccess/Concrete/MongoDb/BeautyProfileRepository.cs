using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class BeautyProfileRepository(MongoDbContext context)
    : MongoDbRepositoryBase<BeautyProfileDocument>(context.MongoConnectionSettings), IBeautyProfileRepository
{
    public async Task<BeautyProfileDocument> GetLatestByAccountIdAsync(ObjectId accountId)
    {
        return await _collection
            .Find(x => x.AccountId == accountId && x.Status == EntityStatus.Active)
            .SortByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<List<BeautyProfileDocument>> GetAllByAccountIdAsync(ObjectId accountId)
    {
        return await _collection
            .Find(x => x.AccountId == accountId && x.Status == EntityStatus.Active)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}