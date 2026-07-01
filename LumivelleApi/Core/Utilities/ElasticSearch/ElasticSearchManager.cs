using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.ElasticSearch.Models;
using Core.Utilities.Results;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Result = Core.Utilities.Results.Result;

namespace Core.Utilities.ElasticSearch;

public class ElasticSearchManager : IElasticSearch
{
    private readonly ElasticsearchClient _client;

    public ElasticSearchManager(IConfiguration configuration)
    {
        var settings = configuration.GetSection("ElasticSearchConfig").Get<ElasticSearchConfig>();
        var clientSettings = new ElasticsearchClientSettings(new System.Uri(settings.ConnectionString))
            .Authentication(new BasicAuthentication(settings.UserName, settings.Password));
        _client = new ElasticsearchClient(clientSettings);
    }

    public async Task<IResult> CreateNewIndexAsync(IndexModel indexModel)
    {
        ValidateIndexName(indexModel.IndexName);

        var exists = await _client.Indices.ExistsAsync(indexModel.IndexName);
        if (exists.Exists)
            return new Result(false, new ResultMessage { Description = "Index already exists" });

        var response = await _client.Indices.CreateAsync(indexModel.IndexName, c =>
        {
            c.Settings(s => s
                .NumberOfReplicas(indexModel.NumberOfReplicas)
                .NumberOfShards(indexModel.NumberOfShards));

            if (!string.IsNullOrEmpty(indexModel.AliasName))
                c.Aliases(a => a.Add(indexModel.AliasName, new Alias()));
        });

        return ToResult(response);
    }

    public async Task<IResult> DeleteByElasticIdAsync(ElasticSearchModel model)
    {
        ValidateIndexName(model.IndexName);
        var response = await _client.DeleteAsync(new DeleteRequest(model.IndexName, model.ElasticId));
        return ToResult(response);
    }

    public async Task<List<ElasticSearchGetModel<T>>> GetAllSearch<T>(SearchParameters parameters)
        where T : class
    {
        ValidateIndexName(parameters.IndexName);
        var response = await _client.SearchAsync<T>(s => s
            .Indices(parameters.IndexName)
            .From(parameters.From)
            .Size(parameters.Size)
            .Query(q => q.MatchAll(_ => { })));

        return Map(response);
    }

    public async Task<IReadOnlyDictionary<string, IndexState>> GetIndexListAsync()
    {
        var response = await _client.Indices.GetAsync(new GetIndexRequest(Indices.All));
        return response.Indices;
    }

    public async Task<List<ElasticSearchGetModel<T>>> GetSearchByField<T>(SearchByFieldParameters fieldParameters)
        where T : class
    {
        ValidateIndexName(fieldParameters.IndexName);
        var response = await _client.SearchAsync<T>(s => s
            .Indices(fieldParameters.IndexName)
            .From(fieldParameters.From)
            .Size(fieldParameters.Size)
            .Query(q => q.Match(m => m
                .Field(fieldParameters.FieldName!)
                .Query(fieldParameters.Value)
                .Operator(Operator.And))));

        return Map(response);
    }

    public async Task<List<ElasticSearchGetModel<T>>> GetSearchBySimpleQueryString<T>(
        SearchByQueryParameters queryParameters)
        where T : class
    {
        ValidateIndexName(queryParameters.IndexName);
        var fields = (queryParameters.Fields ?? Array.Empty<string>()).Select(f => (Field)f).ToArray();

        var response = await _client.SearchAsync<T>(s => s
            .Indices(queryParameters.IndexName)
            .From(queryParameters.From)
            .Size(queryParameters.Size)
            .Query(q => q.SimpleQueryString(sqs => sqs
                .Fields(fields)
                .Query(queryParameters.Query)
                .Analyzer("standard")
                .DefaultOperator(Operator.Or)
                .MinimumShouldMatch("30%")
                .AnalyzeWildcard(false))));

        return Map(response);
    }

    public async Task<IResult> InsertAsync(ElasticSearchInsertUpdateModel model)
    {
        ValidateIndexName(model.IndexName);
        var response = await _client.IndexAsync(model.Item, i => i
            .Index(model.IndexName)
            .Id(model.ElasticId)
            .Refresh(Refresh.True));

        return ToResult(response);
    }

    public async Task<IResult> InsertManyAsync(string indexName, object[] items)
    {
        ValidateIndexName(indexName);
        var response = await _client.BulkAsync(b => b
            .Index(indexName)
            .IndexMany(items));

        return ToResult(response);
    }

    public async Task<IResult> UpdateByElasticIdAsync(ElasticSearchInsertUpdateModel model)
    {
        ValidateIndexName(model.IndexName);
        var response = await _client.UpdateAsync<object, object>(model.IndexName, model.ElasticId,
            u => u.Doc(model.Item));

        return ToResult(response);
    }

    private static List<ElasticSearchGetModel<T>> Map<T>(SearchResponse<T> response) where T : class =>
        response.Hits.Select(h => new ElasticSearchGetModel<T>
        {
            ElasticId = h.Id,
            Item = h.Source
        }).ToList();

    private static IResult ToResult(ElasticsearchResponse response)
    {
        var ok = response.IsValidResponse;
        return new Result(ok, new ResultMessage
        {
            Code = ok ? "Success" : response.ElasticsearchServerError?.Error?.Type ?? "Error",
            Description = ok ? "Success" : response.ElasticsearchServerError?.Error?.Reason ?? "Unknown error"
        });
    }

    private static void ValidateIndexName(string indexName)
    {
        if (string.IsNullOrEmpty(indexName))
            throw new ArgumentNullException(nameof(indexName), "Index name cannot be null or empty");
    }
}
