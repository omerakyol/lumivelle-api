using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface IMakeupLookRepository : IDocumentDbRepository<MakeupLookDocument>
{
    Task<List<MakeupLookDocument>> GetAllAsync();
}
