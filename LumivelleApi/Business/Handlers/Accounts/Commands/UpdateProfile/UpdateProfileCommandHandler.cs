using System;
using System.Threading;
using System.Threading.Tasks; 
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Accounts.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<UpdateProfileCommandRequest, IResult>
{
    [ValidationAspect(typeof(UpdateProfileValidator), Priority = 1)]
    public async Task<IResult> Handle(UpdateProfileCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account = await accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        account.DisplayName = request.DisplayName;
        account.Bio = request.Bio;
        await accountRepository.UpdateAsync(account);

        return new SuccessResult();
    }
}