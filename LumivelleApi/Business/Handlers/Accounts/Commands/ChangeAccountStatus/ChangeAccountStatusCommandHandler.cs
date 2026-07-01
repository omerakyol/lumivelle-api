using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Accounts.Commands.ChangeAccountStatus;

public class ChangeAccountStatusCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<ChangeAccountStatusCommandRequest, IResult>
{
    [AdminOperation(Priority = 1)]
    [ValidationAspect(typeof(ChangeAccountStatusValidator), Priority = 2)]
    public async Task<IResult> Handle(ChangeAccountStatusCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = ObjectId.Parse(request.AccountId);
        var account = await accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        account.AccountStatus = request.Status;
        await accountRepository.UpdateAsync(account.Id, account);

        return new SuccessResult();
    }
}