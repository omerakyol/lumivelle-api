using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Entities.Dtos.Account;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Mapster;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Accounts.Queries.GetAccount;

public class GetAccountQuery : IRequest<IDataResult<AccountDetailDto>>
{
    public string AccountId { get; set; }

    public class GetAccountQueryHandler(
        IAccountRepository accountRepository
    )
        : IRequestHandler<GetAccountQuery, IDataResult<AccountDetailDto>>
    {
        [AdminOperation(Priority = 1)]
        public async Task<IDataResult<AccountDetailDto>> Handle(GetAccountQuery request,
            CancellationToken cancellationToken)
        {
            var accountId = ObjectId.Parse(request.AccountId);
            var data = await accountRepository.GetByIdAsync(accountId);
            if (data == null)
                throw new ApplicationException(Messages.AccountNotFound);

            var account = data.Adapt<AccountDetailDto>();

            return new SuccessDataResult<AccountDetailDto>(account);
        }
    }
}