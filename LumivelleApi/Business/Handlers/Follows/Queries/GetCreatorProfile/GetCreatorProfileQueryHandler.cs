using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Accounts.Queries.GetAccountPublicProfile;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Follows.Queries.GetCreatorProfile;

public class GetCreatorProfileQueryHandler(
    IAccountRepository accountRepository,
    IFollowRepository followRepository,
    IPostRepository postRepository,
    IWardrobeItemRepository wardrobeItemRepository,
    IBeautyProfileRepository beautyProfileRepository)
    : IRequestHandler<GetCreatorProfileQueryRequest, IDataResult<CreatorProfileResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<CreatorProfileResult>> Handle(
        GetCreatorProfileQueryRequest request,
        CancellationToken cancellationToken)
    {
        var viewerAccountId = UserInfoExtensions.GetAccountId();
        var targetAccountId = ObjectId.Parse(request.Id);

        var account = await accountRepository.GetByIdAsync(targetAccountId);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var isOwnProfile = targetAccountId == viewerAccountId;
        var isFollowedByMe = !isOwnProfile && await followRepository.GetAsync(viewerAccountId, targetAccountId) != null;
        var followerCount = await followRepository.CountFollowersAsync(targetAccountId);
        var postCount = await postRepository.CountByAccountIdAsync(targetAccountId);

        var wardrobeItems = await wardrobeItemRepository.GetByAccountIdAsync(targetAccountId, null);
        var styleTag = ComputeTopStyleTag(wardrobeItems);

        var beautyProfile = await beautyProfileRepository.GetLatestByAccountIdAsync(targetAccountId);

        var result = new CreatorProfileResult
        {
            Id = account.Id.ToString(),
            DisplayName = GetAccountPublicProfileQueryHandler.ToDisplayName(account),
            AvatarUrl = account.PhotoUrl,
            Bio = account.Bio,
            IsVerified = account.IsVerified,
            IsCreator = account.IsCreator,
            StyleTag = styleTag,
            Season = beautyProfile?.Season,
            PostCount = postCount,
            FollowerCount = followerCount,
            IsFollowedByMe = isFollowedByMe,
            IsOwnProfile = isOwnProfile
        };

        return new SuccessDataResult<CreatorProfileResult>(result);
    }

    private static string ComputeTopStyleTag(List<WardrobeItemDocument> items)
    {
        var weights = new Dictionary<string, int>();

        foreach (var item in items)
        foreach (var tag in item.StyleTags)
            weights[tag] = weights.GetValueOrDefault(tag) + item.WearCount;

        return weights.Count == 0 ? null : weights.OrderByDescending(kv => kv.Value).First().Key;
    }
}