using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface IDailyEditPresetRepository : IDocumentDbRepository<DailyEditPresetDocument>
{
    Task<List<DailyEditPresetDocument>> GetByBucketAsync(string seasonFamily, string undertone, string contrast);
}
