using System;
using System.Collections.Generic; 
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Account;
using Core.Enums;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Business.Handlers.Accounts.Queries.GetAccounts;

public class GetAccountsQuery : PaginationFilter, IRequest<PaginatedResult<List<AccountDto>>>
{
    [FromQuery(Name = "status")] public AccountStatus? Status { get; set; }
    [FromQuery(Name = "accountType")] public AccountType? AccountType { get; set; }
    [FromQuery(Name = "query")] public string? Query { get; set; }

    [FromQuery(Name = "startDate")] public DateTime? StartDate { get; set; }
    [FromQuery(Name = "endDate")] public DateTime? EndDate { get; set; }

    public class GetAccountsQueryHandler(IAccountRepository accountRepository)
        : IRequestHandler<GetAccountsQuery, PaginatedResult<List<AccountDto>>>
    {
        [AdminOperation(Priority = 1)]
        public async Task<PaginatedResult<List<AccountDto>>> Handle(GetAccountsQuery request,
            CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Query))
                request.Query = request.Query.Trim().ToLower();

            if (request.StartDate.HasValue)
                request.StartDate = request.StartDate.Value.Date;
            if (request.EndDate.HasValue)
                request.EndDate = request.EndDate.Value.Date.AddDays(1);

            Expression<Func<Account, bool>> filter = x =>
                (request.Query == null ||
                 x.Email.Contains(request.Query, StringComparison.CurrentCultureIgnoreCase)) &&
                (request.StartDate == null || x.CreatedAt > request.StartDate) &&
                (request.EndDate == null || x.CreatedAt < request.EndDate) &&
                (request.AccountType == null || x.AccountType == request.AccountType) &&
                (request.Status == null || x.AccountStatus == request.Status);

            var paginationData = await accountRepository.GetPaginatedListAsync(request, filter,
                Builders<Account>.Sort.Descending(x => x.CreatedAt));
            if (paginationData.Data.Count == 0)
                return new PaginatedResult<List<AccountDto>>([], paginationData.TotalRecords, paginationData.PageNumber,
                    paginationData.PageSize);

            var data = paginationData.Data.Adapt<List<AccountDto>>();

            return new PaginatedResult<List<AccountDto>>(data, paginationData.TotalRecords, paginationData.PageNumber,
                paginationData.PageSize);
        }
    }
}