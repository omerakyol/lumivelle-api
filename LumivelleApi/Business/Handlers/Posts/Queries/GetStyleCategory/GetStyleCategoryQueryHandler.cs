using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetStyleCategory;

public class GetStyleCategoryQueryHandler(
    IPostRepository postRepository,
    IWardrobeItemRepository wardrobeItemRepository,
    IOutfitRepository outfitRepository,
    IPostLikeRepository postLikeRepository,
    ISavedPostRepository savedPostRepository,
    IAccountRepository accountRepository,
    IFollowRepository followRepository)
    : IRequestHandler<GetStyleCategoryQueryRequest, IDataResult<StyleCategoryPageResult>>
{
    private const int SecondaryTagCount = 4;

    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<StyleCategoryPageResult>> Handle(
        GetStyleCategoryQueryRequest request,
        CancellationToken cancellationToken)
    {
        var viewerAccountId = UserInfoExtensions.GetAccountId();

        var allPosts = await postRepository.GetActiveInWindowAsync(null);
        var tagsByPostId = await PostStyleTagResolver.GetTagsByPostIdAsync(allPosts, wardrobeItemRepository, outfitRepository);

        var primaryMatches = allPosts
            .Where(p => tagsByPostId.TryGetValue(p.Id, out var tags) && tags.Contains(request.StyleTag))
            .OrderByDescending(p => p.CreatedAt)
            .ToList();

        var totalPostCount = primaryMatches.Count;

        string[] secondaryTags = [];
        if (string.IsNullOrEmpty(request.Cursor))
        {
            secondaryTags = primaryMatches
                .Take(PostResultBuilder.PageSize)
                .SelectMany(p => tagsByPostId[p.Id])
                .Where(t => t != request.StyleTag)
                .GroupBy(t => t)
                .OrderByDescending(g => g.Count())
                .Take(SecondaryTagCount)
                .Select(g => g.Key)
                .ToArray();
        }

        var filteredMatches = string.IsNullOrEmpty(request.SecondaryTag)
            ? primaryMatches
            : primaryMatches.Where(p => tagsByPostId[p.Id].Contains(request.SecondaryTag)).ToList();

        var cursor = PostResultBuilder.ParseCursor(request.Cursor);
        var pagePosts = (cursor.HasValue ? filteredMatches.Where(p => p.CreatedAt < cursor.Value) : filteredMatches)
            .Take(PostResultBuilder.PageSize)
            .ToList();

        var page = await PostResultBuilder.BuildPageAsync(
            pagePosts, viewerAccountId, postLikeRepository, savedPostRepository, accountRepository, followRepository);

        var result = new StyleCategoryPageResult
        {
            StyleTag = request.StyleTag,
            TotalPostCount = totalPostCount,
            SecondaryTags = secondaryTags,
            Page = page
        };

        return new SuccessDataResult<StyleCategoryPageResult>(result);
    }
}
