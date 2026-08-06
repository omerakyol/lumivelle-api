using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Enums;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class ColorPaletteRepository : MongoDbRepositoryBase<ColorPaletteDocument>, IColorPaletteRepository
{
    public ColorPaletteRepository(MongoDbContext context) : base(context.MongoConnectionSettings)
    {
    }

    public async Task<List<ColorPaletteDocument>> GetAllAsync()
    {
        return await _collection.Find(x => x.Status == EntityStatus.Active).ToListAsync();
    }

    public async Task<ColorPaletteDocument> GetBySeasonAsync(string season)
    {
        return await _collection.Find(x => x.Season == season && x.Status == EntityStatus.Active).FirstOrDefaultAsync();
    }
}
