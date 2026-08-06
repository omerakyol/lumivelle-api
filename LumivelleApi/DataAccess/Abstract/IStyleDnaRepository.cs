using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface IStyleDnaRepository : IDocumentDbRepository<StyleDnaDocument>
{
    Task<List<StyleDnaDocument>> GetAllAsync();
}
