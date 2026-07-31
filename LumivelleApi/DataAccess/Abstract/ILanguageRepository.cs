using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface ILanguageRepository : IDocumentDbRepository<Language>
{
    Task<List<Language>> GetAllAsync();
}
