using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface IColorPaletteRepository : IDocumentDbRepository<ColorPaletteDocument>
{
    Task<List<ColorPaletteDocument>> GetAllAsync();
    Task<ColorPaletteDocument> GetBySeasonAsync(string season);
}
