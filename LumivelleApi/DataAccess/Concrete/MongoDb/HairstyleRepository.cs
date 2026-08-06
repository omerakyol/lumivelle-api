using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Enums;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class HairstyleRepository : MongoDbRepositoryBase<HairstyleDocument>, IHairstyleRepository
{
    public HairstyleRepository(MongoDbContext context) : base(context.MongoConnectionSettings)
    {
    }

    public async Task<List<HairstyleDocument>> GetAllAsync()
    {
        return await _collection.Find(x => x.Status == EntityStatus.Active).SortBy(x => x.SortOrder).ToListAsync();
    }
}
