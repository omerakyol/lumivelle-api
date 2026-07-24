using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Handlers.Accounts.Queries.GetAccountPublicProfile;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using MongoDB.Bson;

namespace Business.Handlers.Posts;

public static class AuthorLookup
{
    public static AuthorResult ToAuthorResult(Account account, bool isFollowedByMe)
    {
        if (account == null)
            return null;

        return new AuthorResult
        {
            Id = account.Id.ToString(),
            Name = GetAccountPublicProfileQueryHandler.ToDisplayName(account),
            AvatarUrl = account.PhotoUrl,
            IsFollowedByMe = isFollowedByMe
        };
    }

    public static async Task<Dictionary<string, AuthorResult>> GetAuthorsAsync(
        IAccountRepository accountRepository,
        IFollowRepository followRepository,
        ObjectId viewerAccountId,
        IEnumerable<ObjectId> accountIds)
    {
        var ids = accountIds.Distinct().ToList();
        if (ids.Count == 0)
            return new Dictionary<string, AuthorResult>();

        var accounts = await accountRepository.GetListAsync(a => ids.Contains(a.Id));
        var followedIds = await followRepository.GetFollowedIdsAsync(viewerAccountId, ids);

        return accounts.ToDictionary(a => a.Id.ToString(), a => ToAuthorResult(a, followedIds.Contains(a.Id)));
    }
}
