using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Collections.Queries.GetCollections;

public class GetCollectionsQueryHandler(
    ICollectionRepository collectionRepository,
    ISavedPostRepository savedPostRepository,
    IPostRepository postRepository)
    : IRequestHandler<GetCollectionsQueryRequest, IDataResult<List<CollectionResult>>>
{
    private const int PreviewCount = 4;

    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<List<CollectionResult>>> Handle(
        GetCollectionsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();

        var results = new List<CollectionResult>
        {
            await BuildAllSavedResultAsync(accountId)
        };

        var collections = await collectionRepository.GetByAccountIdAsync(accountId);
        foreach (var collection in collections)
        {
            results.Add(await BuildCollectionResultAsync(accountId, collection));
        }

        return new SuccessDataResult<List<CollectionResult>>(results);
    }

    private async Task<CollectionResult> BuildAllSavedResultAsync(ObjectId accountId)
    {
        var postCount = await savedPostRepository.CountByAccountAndCollectionAsync(accountId, null);
        var previewImageUrls = await BuildPreviewImageUrlsAsync(accountId, null);

        return new CollectionResult
        {
            Id = "all-saved",
            Name = "All saved",
            PostCount = postCount,
            PreviewImageUrls = previewImageUrls,
            IsDefault = true
        };
    }

    private async Task<CollectionResult> BuildCollectionResultAsync(ObjectId accountId, CollectionDocument collection)
    {
        var postCount = await savedPostRepository.CountByAccountAndCollectionAsync(accountId, collection.Id);
        var previewImageUrls = await BuildPreviewImageUrlsAsync(accountId, collection.Id);

        return CollectionResult.FromDocument(collection, postCount, previewImageUrls);
    }

    private async Task<string[]> BuildPreviewImageUrlsAsync(ObjectId accountId, ObjectId? collectionId)
    {
        var saves = await savedPostRepository.GetByAccountAndCollectionPageAsync(accountId, collectionId, null, PreviewCount);
        if (saves.Count == 0)
            return [];

        var postsById = (await postRepository.GetByIdsAsync(saves.Select(s => s.PostId))).ToDictionary(p => p.Id);

        return saves
            .Where(s => postsById.ContainsKey(s.PostId))
            .Select(s => postsById[s.PostId].ImageUrls.FirstOrDefault())
            .Where(url => !string.IsNullOrEmpty(url))
            .ToArray();
    }
}
