using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface IShadeRepository : IDocumentDbRepository<ShadeDocument>
{
    Task<List<ShadeDocument>> GetByCategoryAsync(string category);
}
