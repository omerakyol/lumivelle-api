using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Accounts.Commands.RegisterFirebaseToken;

public class RegisterFirebaseTokenCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<RegisterFirebaseTokenCommandRequest, IResult>
{
    [ValidationAspect(typeof(RegisterFirebaseTokenValidator), Priority = 1)]
    public async Task<IResult> Handle(RegisterFirebaseTokenCommandRequest request, CancellationToken cancellationToken)
    {
        var currentAccountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == currentAccountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        if (account.FirebaseToken == request.FirebaseToken) return new SuccessResult();

        account.FirebaseToken = request.FirebaseToken;
        await accountRepository.UpdateAsync(account);

        return new SuccessResult();
    }
}