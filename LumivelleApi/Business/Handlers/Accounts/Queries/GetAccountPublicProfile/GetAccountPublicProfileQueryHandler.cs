using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Accounts.Queries.GetAccountPublicProfile;

public class GetAccountPublicProfileQueryHandler(IAccountRepository accountRepository)
    : IRequestHandler<GetAccountPublicProfileQueryRequest, IDataResult<AccountPublicProfileResult>>
{
    public async Task<IDataResult<AccountPublicProfileResult>> Handle(
        GetAccountPublicProfileQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = ObjectId.Parse(request.Id);
        var account = await accountRepository.GetByIdAsync(accountId);

        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var result = new AccountPublicProfileResult
        {
            Id = account.Id.ToString(),
            Name = ToDisplayName(account.Email),
            AvatarUrl = account.PhotoUrl
        };

        return new SuccessDataResult<AccountPublicProfileResult>(result);
    }

    internal static string ToDisplayName(string email)
    {
        // The Account entity has no dedicated display-name field in this
        // codebase (Phase 1/2 never added one) — the local part of the
        // email is used as a pragmatic stand-in.
        if (string.IsNullOrEmpty(email))
            return string.Empty;

        var atIndex = email.IndexOf('@');
        return atIndex > 0 ? email[..atIndex] : email;
    }
}
