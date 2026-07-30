using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Concrete;
using Core.Enums;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Concrete.MongoDb;

public class CommentRepository(MongoDbContext context)
    : MongoDbRepositoryBase<CommentDocument>(context.MongoConnectionSettings), ICommentRepository
{
    public async Task<List<CommentDocument>> GetByPostIdPageAsync(ObjectId postId, DateTime? cursor, int pageSize)
    {
        var filter = Builders<CommentDocument>.Filter.Eq(x => x.PostId, postId)
                     & Builders<CommentDocument>.Filter.Eq(x => x.Status, EntityStatus.Active);
        if (cursor.HasValue)
            filter &= Builders<CommentDocument>.Filter.Gt(x => x.CreatedAt, cursor.Value);

        return await _collection.Find(filter)
            .SortBy(x => x.CreatedAt)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task DeleteAllByPostIdAsync(ObjectId postId)
    {
        // Hard delete: cascade cleanup when the parent post is deleted, not a
        // user-initiated single-record delete, so the soft-delete convention
        // does not apply here.
        var filter = Builders<CommentDocument>.Filter.Eq(x => x.PostId, postId);
        await _collection.DeleteManyAsync(filter);
    }
}