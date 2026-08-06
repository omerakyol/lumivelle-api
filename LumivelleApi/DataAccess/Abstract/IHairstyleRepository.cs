using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface IHairstyleRepository : IDocumentDbRepository<HairstyleDocument>
{
    Task<List<HairstyleDocument>> GetAllAsync();
}
