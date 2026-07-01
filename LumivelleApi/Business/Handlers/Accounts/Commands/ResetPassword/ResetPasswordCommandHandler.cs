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
using MongoDB.Bson;

namespace Business.Handlers.Accounts.Commands.ResetPassword;

public class ResetPasswordCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<ResetPasswordCommandRequest, IResult>
{
    [ValidationAspect(typeof(ResetPasswordValidator), Priority = 1)]
    public async Task<IResult> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var accountType = UserInfoExtensions.GetAccountType();
        var currentAccountId = UserInfoExtensions.GetAccountId();
        if (accountType == AccountType.Admin)
            currentAccountId = ObjectId.Parse(request.AccountId);

        var account =
            await accountRepository.GetAsync(x => x.Id == currentAccountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        account.Password = hashedPassword;

        await accountRepository.UpdateAsync(account.Id, account);

        return new SuccessResult(new ResultMessage { Code = Messages.Updated, Description = Messages.Updated });
    }
}