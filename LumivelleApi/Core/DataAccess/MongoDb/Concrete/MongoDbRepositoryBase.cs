using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Constants;
using Core.DataAccess.MongoDb.Concrete.Configurations;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Messages;
using Core.Utilities.Results;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Core.DataAccess.MongoDb.Concrete;

public abstract class MongoDbRepositoryBase<T> : IDocumentDbRepository<T>
    where T : DocumentDbEntity
{
    protected readonly IMongoCollection<T> _collection;
    protected string CollectionName { get; set; }

    private string ToCamelCase(string name)
    {
        if (string.IsNullOrEmpty(name) || name.Length < 2)
            return name.ToLower();

        return char.ToLowerInvariant(name[0]) + name.Substring(1);
    }

    protected MongoDbRepositoryBase(MongoConnectionSettings mongoConnectionSetting, string? collectionName = null)
    {
        CollectionName = collectionName ?? $"{ToCamelCase(typeof(T).Name)}s";

        ConnectionSettingControl(mongoConnectionSetting);


        var client = mongoConnectionSetting.GetMongoClientSettings() == null
            ? new MongoClient(mongoConnectionSetting.ConnectionString)
            : new MongoClient(mongoConnectionSetting.GetMongoClientSettings());


        var database = client.GetDatabase(mongoConnectionSetting.DatabaseName);
        _collection = database.GetCollection<T>(CollectionName);
    }

    public bool Any(Expression<Func<T, bool>> predicate = null, bool allRecords = false)
    {
        var query = _collection.AsQueryable();
        if (!allRecords) query = query.Where(x => x.Status == EntityStatus.Active);

        if (predicate != null)
            query = query.Where(predicate);

        return query.FirstOrDefault() != null;
    }

    public virtual void Delete(ObjectId id, bool softDelete = true)
    {
        if (softDelete)
        {
            var record = GetById(id);
            if (record == null)
                throw new ApplicationException(Messages.NotFound);

            record.Status = EntityStatus.Deleted;
            record.DeletedAt = DateTime.UtcNow;
            record.DeletedBy = UserInfoExtensions.GetAccountId();
            Update(record.Id, record);
        }
        else
        {
            _collection.FindOneAndDelete(x => x.Id == id);
        }
    }

    public virtual void Delete(T record, bool softDelete = true)
    {
        if (softDelete)
        {
            record.Status = EntityStatus.Deleted;
            record.DeletedAt = DateTime.UtcNow;
            record.DeletedBy = UserInfoExtensions.GetAccountId();
            Update(record.Id, record);
        }
        else
        {
            _collection.FindOneAndDelete(x => x.Id == record.Id);
        }
    }

    public virtual async Task DeleteAsync(ObjectId id, bool softDelete = true)
    {
        if (softDelete)
        {
            var record = await GetByIdAsync(id);
            if (record == null)
                throw new ApplicationException(Messages.NotFound);

            record.Status = EntityStatus.Deleted;
            record.DeletedAt = DateTime.UtcNow;
            record.DeletedBy = UserInfoExtensions.GetAccountId();
            await UpdateAsync(record);
        }
        else
        {
            await _collection.FindOneAndDeleteAsync(x => x.Id == id);
        }
    }

    public virtual async Task DeleteAsync(T record, bool softDelete = true)
    {
        if (softDelete)
        {
            record.Status = EntityStatus.Deleted;
            record.DeletedAt = DateTime.UtcNow;
            record.DeletedBy = UserInfoExtensions.GetAccountId();
            await UpdateAsync(record);
        }
        else
        {
            await _collection.FindOneAndDeleteAsync(x => x.Id == record.Id);
        }
    }

    public async Task<PaginatedResult<List<T>>> GetPaginatedListAsync(PaginationFilter pagination,
        Expression<Func<T, bool>> filter = null, SortDefinition<T> sort = null, bool allRecords = false)
    {
        var filters = new List<FilterDefinition<T>>();

        if (filter != null)
            filters.Add(Builders<T>.Filter.Where(filter));

        if (!allRecords) filters.Add(Builders<T>.Filter.Eq(x => x.Status, EntityStatus.Active));

        var allFilters = filters.Count switch
        {
            0 => Builders<T>.Filter.Empty,
            1 => filters[0],
            _ => Builders<T>.Filter.And(filters)
        };

        var totalItemCount = await _collection.CountDocumentsAsync(allFilters);

        var query = _collection.Find(allFilters);
        if (sort != null)
            query = query.Sort(sort);

        var pageNumber = pagination.PageNumber - 1;
        var pageSize = pagination.PageSize;

        var data = await query.Skip(pageNumber * pageSize).Limit(pageSize).ToListAsync();
        return new PaginatedResult<List<T>>(data, (int)totalItemCount, pagination.PageNumber, pagination.PageSize);
    }
    
    public async Task<PaginatedResult<List<T>>> GetPaginatedListAsync(PaginationFilter pagination,
        FilterDefinition<T> filter = null, SortDefinition<T> sort = null, bool allRecords = false)
    {
        var filters = new List<FilterDefinition<T>>();

        if (filter != null)
            filters.Add(filter);

        if (!allRecords) filters.Add(Builders<T>.Filter.Eq(x => x.Status, EntityStatus.Active));

        var allFilters = filters.Count switch
        {
            0 => Builders<T>.Filter.Empty,
            1 => filters[0],
            _ => Builders<T>.Filter.And(filters)
        };

        var totalItemCount = await _collection.CountDocumentsAsync(allFilters);

        var query = _collection.Find(allFilters);
        if (sort != null)
            query = query.Sort(sort);

        var pageNumber = pagination.PageNumber - 1;
        var pageSize = pagination.PageSize;

        var data = await query.Skip(pageNumber * pageSize).Limit(pageSize).ToListAsync();
        return new PaginatedResult<List<T>>(data, (int)totalItemCount, pagination.PageNumber, pagination.PageSize);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool allRecords = false)
    {
        var filters = new List<FilterDefinition<T>>();

        if (filter != null)
            filters.Add(Builders<T>.Filter.Where(filter));

        if (!allRecords) filters.Add(Builders<T>.Filter.Eq(x => x.Status, EntityStatus.Active));

        var allFilters = filters.Count switch
        {
            0 => Builders<T>.Filter.Empty,
            1 => filters[0],
            _ => Builders<T>.Filter.And(filters)
        };

        return await _collection.Find(allFilters).SortByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
    }


    public virtual T GetById(ObjectId id, bool allRecords = false)
    {
        return _collection.Find(x => x.Id == id).SortByDescending(x => x.CreatedAt).FirstOrDefault();
    }

    public virtual async Task<T> GetByIdAsync(ObjectId id, bool allRecords = false)
    {
        return await _collection.Find(x => x.Id == id).SortByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
    }

    public virtual void Add(T entity)
    {
        entity.Id = ObjectId.GenerateNewId(); 
        entity.Status = EntityStatus.Active;
        entity.CreatedBy = UserInfoExtensions.GetAccountId();
        var options = new InsertOneOptions { BypassDocumentValidation = false };
        _collection.InsertOne(entity, options);
    }

    public virtual async Task AddAsync(T entity)
    {
        entity.Id = ObjectId.GenerateNewId(); 
        entity.Status = EntityStatus.Active;
        entity.CreatedBy = UserInfoExtensions.GetAccountId();
        var options = new InsertOneOptions { BypassDocumentValidation = false };
        await _collection.InsertOneAsync(entity, options);
    }

    public virtual void AddMany(IEnumerable<T> entities)
    {
        var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
        var documentDbEntities = entities.ToList();
        foreach (var entity in documentDbEntities)
        {
            entity.Id = ObjectId.GenerateNewId();
            entity.Status = EntityStatus.Active;
            entity.CreatedBy = UserInfoExtensions.GetAccountId();
        }

        var writeModels = documentDbEntities.Select(entity => new InsertOneModel<T>(entity)).ToList();
        _collection.BulkWriteAsync(writeModels, options).GetAwaiter().GetResult();
    }

    public virtual async Task AddManyAsync(IEnumerable<T> entities)
    {
        var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
        var dbEntities = entities.ToList();
        foreach (var entity in dbEntities)
        {
            entity.Id = ObjectId.GenerateNewId();
            entity.Status = EntityStatus.Active;
            entity.CreatedBy = UserInfoExtensions.GetAccountId();
        }

        var writeModels = dbEntities.Select(entity => new InsertOneModel<T>(entity)).ToList();
        await _collection.BulkWriteAsync(writeModels, options);
    }

    public async Task UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
    {
        await _collection.UpdateManyAsync(filter, update);
    }


    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null, SortDefinition<T> sort = null,
        bool allRecords = false)
    {
        var filters = new List<FilterDefinition<T>>();

        if (filter != null)
            filters.Add(Builders<T>.Filter.Where(filter));

        if (!allRecords) filters.Add(Builders<T>.Filter.Eq(x => x.Status, EntityStatus.Active));

        var allFilters = filters.Count switch
        {
            0 => Builders<T>.Filter.Empty,
            1 => filters[0],
            _ => Builders<T>.Filter.And(filters)
        };

        var query = _collection.Find(allFilters);
        if (sort != null)
            query = query.Sort(sort);

        return await query.ToListAsync();
    }

    public virtual void Update(ObjectId id, T record)
    {
        record.UpdatedAt = DateTime.UtcNow;
        _collection.FindOneAndReplace(x => x.Id == id, record);
    }

    public virtual void Update(T record, Expression<Func<T, bool>> predicate)
    {
        record.UpdatedAt = DateTime.UtcNow;
        record.UpdatedBy = UserInfoExtensions.GetAccountId();
        _collection.FindOneAndReplace(predicate, record);
    }

    public virtual async Task UpdateAsync(ObjectId id, T record)
    {
        record.UpdatedAt = DateTime.UtcNow;
        record.UpdatedBy = UserInfoExtensions.GetAccountId();
        await _collection.FindOneAndReplaceAsync(x => x.Id == id, record);
    }

    public virtual async Task UpdateAsync(T record, Expression<Func<T, bool>> predicate = null)
    {
        record.UpdatedAt = DateTime.UtcNow;
        record.UpdatedBy = UserInfoExtensions.GetAccountId();
        if (predicate != null)
            await _collection.FindOneAndReplaceAsync(predicate, record);
        else
            await _collection.FindOneAndReplaceAsync(x => x.Id == record.Id, record);
    }

    public async Task<long> CountAsync(Expression<Func<T, bool>> filter)
    {
        return await _collection.CountDocumentsAsync(filter);
    }

    public async Task<IAsyncCursor<T>> Aggregate(PipelineDefinition<T, T> pipeline)
    {
        return await _collection.AggregateAsync<T>(pipeline);
    }

    private void ConnectionSettingControl(MongoConnectionSettings settings)
    {
        if (settings.GetMongoClientSettings() != null &&
            (string.IsNullOrEmpty(CollectionName) || string.IsNullOrEmpty(settings.DatabaseName)))
            throw new Exception(DocumentDbMessages.NullOremptyMessage);

        if (string.IsNullOrEmpty(CollectionName) ||
            string.IsNullOrEmpty(settings.ConnectionString) ||
            string.IsNullOrEmpty(settings.DatabaseName))
            throw new Exception(DocumentDbMessages.NullOremptyMessage);
    }
}