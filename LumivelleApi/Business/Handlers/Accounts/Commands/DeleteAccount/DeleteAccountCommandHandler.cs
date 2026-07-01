using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Enums;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<DeleteAccountCommandRequest, IResult>
{
    [AdminOperation(Priority = 1)]
    [ValidationAspect(typeof(DeleteAccountValidator), Priority = 2)]
    public async Task<IResult> Handle(DeleteAccountCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = ObjectId.Parse(request.AccountId);
        var account = await accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);
        
        await accountRepository.DeleteAsync(account.Id);
        return new SuccessResult();
    }
}