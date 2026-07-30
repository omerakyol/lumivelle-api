using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface IBeautyProfileRepository : IDocumentDbRepository<BeautyProfileDocument>
{
    Task<BeautyProfileDocument> GetLatestByAccountIdAsync(ObjectId accountId);
    Task<List<BeautyProfileDocument>> GetAllByAccountIdAsync(ObjectId accountId);
}