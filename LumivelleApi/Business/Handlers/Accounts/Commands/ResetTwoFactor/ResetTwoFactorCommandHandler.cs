using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Accounts.ValidationRules;
using Business.Helpers;

using Core.Aspects.Autofac.Validation;
using Core.Constants;

using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Accounts.Commands.ResetTwoFactor;

public class ResetTwoFactorCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<ResetTwoFactorCommandRequest, IResult>
{
    [AdminOperation(Priority = 1)]
    [ValidationAspect(typeof(ResetTwoFactorValidator), Priority = 2)]

    public async Task<IResult> Handle(ResetTwoFactorCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = ObjectId.Parse(request.AccountId);
        var account = await accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        account.TwoFactorEnabled = false;
        account.TwoFactorSecretKey = null;
        account.Last2FaVerifiedAt = null;
        await accountRepository.UpdateAsync(account);

        return new SuccessResult();
    }
}