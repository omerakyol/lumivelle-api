using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Core.DataAccess;

public interface IDocumentDbRepository<T>
    where T : DocumentDbEntity
{
    void Add(T entity);

    Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null, SortDefinition<T> sort = null,
        bool allRecords = false);

    Task<PaginatedResult<List<T>>> GetPaginatedListAsync(PaginationFilter pagination,
        Expression<Func<T, bool>> filter = null, SortDefinition<T> sort = null, bool allRecords = false);

    Task<PaginatedResult<List<T>>> GetPaginatedListAsync(PaginationFilter pagination,
        FilterDefinition<T> filter = null, SortDefinition<T> sort = null, bool allRecords = false);

    Task<T> GetAsync(Expression<Func<T, bool>> filter, bool allRecords = false);
    T GetById(ObjectId id, bool allRecords = false);
    void AddMany(IEnumerable<T> entities);
    void Update(ObjectId id, T record);
    void Update(T record, Expression<Func<T, bool>> predicate);
    Task UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);
    void Delete(ObjectId id, bool softDelete = true);
    void Delete(T record, bool softDelete = true);
    Task AddAsync(T entity);
    Task<T> GetByIdAsync(ObjectId id, bool allRecords = false);
    Task AddManyAsync(IEnumerable<T> entities);
    Task UpdateAsync(ObjectId id, T record);
    Task UpdateAsync(T record, Expression<Func<T, bool>> predicate = null);
    Task DeleteAsync(ObjectId id, bool softDelete = true);
    Task DeleteAsync(T record, bool softDelete = true);
    bool Any(Expression<Func<T, bool>> predicate = null, bool allRecords = false);
    Task<long> CountAsync(Expression<Func<T, bool>> filter);
    Task<IAsyncCursor<T>> Aggregate(PipelineDefinition<T, T> pipeline);
}