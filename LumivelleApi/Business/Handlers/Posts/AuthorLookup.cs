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
    public static AuthorResult ToAuthorResult(Account account)
    {
        if (account == null)
            return null;

        return new AuthorResult
        {
            Id = account.Id.ToString(),
            Name = GetAccountPublicProfileQueryHandler.ToDisplayName(account.Email),
            AvatarUrl = account.PhotoUrl
        };
    }

    public static async Task<Dictionary<string, AuthorResult>> GetAuthorsAsync(
        IAccountRepository accountRepository, IEnumerable<ObjectId> accountIds)
    {
        var ids = accountIds.Distinct().ToList();
        if (ids.Count == 0)
            return new Dictionary<string, AuthorResult>();

        var accounts = await accountRepository.GetListAsync(a => ids.Contains(a.Id));

        return accounts.ToDictionary(a => a.Id.ToString(), ToAuthorResult);
    }
}
