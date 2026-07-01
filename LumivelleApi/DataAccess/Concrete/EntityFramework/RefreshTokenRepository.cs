using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;

namespace DataAccess.Concrete.EntityFramework;

public class RefreshTokenRepository(MongoDbContext context)
    : MongoDbRepositoryBase<RefreshToken>(context.MongoConnectionSettings), IRefreshTokenRepository
{
}