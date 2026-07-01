using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;

namespace DataAccess.Concrete.EntityFramework;

public class LogRepository(MongoDbContext context)
    : MongoDbRepositoryBase<Log>(context.MongoConnectionSettings), ILogRepository
{
}