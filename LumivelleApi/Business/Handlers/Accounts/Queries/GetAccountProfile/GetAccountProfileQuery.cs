using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Core.Entities.Dtos.Account;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Accounts.Queries.GetAccountProfile;

public class GetAccountProfileQuery : IRequest<IDataResult<AccountProfileDto>>
{
    public class GetAccountProfileQueryHandler(
        IAccountRepository accountRepository,
        IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<GetAccountProfileQuery, IDataResult<AccountProfileDto>>
    {
        public async Task<IDataResult<AccountProfileDto>> Handle(GetAccountProfileQuery request,
            CancellationToken cancellationToken)
        {
            var accountId = UserInfoExtensions.GetAccountId();
            var data = await accountRepository.GetByIdAsync(accountId);
            if (data == null)
                throw new ApplicationException(Messages.AccountNotFound);

            var account = data.Adapt<AccountProfileDto>();

            if (string.IsNullOrEmpty(account.PhotoUrl)) return new SuccessDataResult<AccountProfileDto>(account);

            var contextRequest = httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{contextRequest?.Scheme}://{contextRequest?.Host.Value}";
            var fileUrl = $"{baseUrl}/media/{account.PhotoUrl}";
            account.PhotoUrl = fileUrl;

            return new SuccessDataResult<AccountProfileDto>(account);
        }
    }
}