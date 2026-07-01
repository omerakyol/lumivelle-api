using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;

namespace DataAccess.Concrete.EntityFramework;

public class TranslateRepository(MongoDbContext context)
    : MongoDbRepositoryBase<Translate>(context.MongoConnectionSettings), ITranslateRepository
{
    public async Task<List<Translate>> GetTranslates(string? languageCode = null)
    {
        var data = string.IsNullOrEmpty(languageCode)
            ? await GetListAsync()
            : await GetListAsync(x => x.Language == languageCode);
        return data;
    }
    
    public async Task<Translate> GetTranslate(string code, string? languageCode = "en")
    {
        return await GetAsync(x => x.Code == code && x.Language == languageCode); 
    }
}