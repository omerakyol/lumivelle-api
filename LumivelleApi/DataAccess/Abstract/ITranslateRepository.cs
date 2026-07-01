using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface ITranslateRepository : IDocumentDbRepository<Translate>
{
    Task<List<Translate>> GetTranslates(string? languageCode = null);
    Task<Translate> GetTranslate(string code, string? languageCode = "en");
}