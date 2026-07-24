using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Accounts.Commands.SetAccountFlags;

public class SetAccountFlagsCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<SetAccountFlagsCommandRequest, IResult>
{
    [AdminOperation(Priority = 1)]
    public async Task<IResult> Handle(SetAccountFlagsCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = ObjectId.Parse(request.Id);
        var account = await accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        account.IsVerified = request.IsVerified;
        account.IsCreator = request.IsCreator;
        await accountRepository.UpdateAsync(account);

        return new SuccessResult();
    }
}
