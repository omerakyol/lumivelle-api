using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Enums;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class ShadeRepository : MongoDbRepositoryBase<ShadeDocument>, IShadeRepository
{
    public ShadeRepository(MongoDbContext context)
        : base(context.MongoConnectionSettings)
    {
    }

    public async Task<List<ShadeDocument>> GetByCategoryAsync(string category)
    {
        return await _collection
            .Find(x => x.Category == category && x.Status == EntityStatus.Active)
            .SortBy(x => x.SortOrder)
            .ToListAsync();
    }
}
