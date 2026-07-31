using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class LanguageRepository(MongoDbContext context)
    : MongoDbRepositoryBase<Language>(context.MongoConnectionSettings), ILanguageRepository
{
    public async Task<List<Language>> GetAllAsync()
    {
        return await _collection
            .Find(x => x.Status == EntityStatus.Active)
            .SortBy(x => x.Name)
            .ToListAsync();
    }
}
