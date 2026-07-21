using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetSavedPosts;

public class GetSavedPostsQueryHandler(
    ISavedPostRepository savedPostRepository,
    IPostRepository postRepository,
    IPostLikeRepository postLikeRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetSavedPostsQueryRequest, IDataResult<FeedPageResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<FeedPageResult>> Handle(
        GetSavedPostsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var cursor = PostResultBuilder.ParseCursor(request.Cursor);

        var savedPage = await savedPostRepository.GetByAccountIdPageAsync(
            accountId, cursor, PostResultBuilder.PageSize);

        if (savedPage.Count == 0)
            return new SuccessDataResult<FeedPageResult>(new FeedPageResult { Posts = [], NextCursor = null });

        // Fetch the referenced posts and re-order them to match the
        // newest-saved-first order of `savedPage` (GetByIdsAsync is unordered).
        var postsById = (await postRepository.GetByIdsAsync(savedPage.Select(s => s.PostId)))
            .ToDictionary(p => p.Id);

        var orderedPosts = savedPage
            .Where(s => postsById.ContainsKey(s.PostId))
            .Select(s => postsById[s.PostId])
            .ToList();

        var results = await PostResultBuilder.ToResultsAsync(
            orderedPosts, accountId, postLikeRepository, savedPostRepository, accountRepository);

        return new SuccessDataResult<FeedPageResult>(new FeedPageResult
        {
            Posts = results,
            NextCursor = savedPage.Count < PostResultBuilder.PageSize ? null : savedPage[^1].CreatedAt.ToString("o")
        });
    }
}
