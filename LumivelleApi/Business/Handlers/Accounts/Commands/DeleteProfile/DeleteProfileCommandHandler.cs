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

namespace Business.Handlers.Accounts.Commands.DeleteProfile;

public class DeleteProfileCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<DeleteProfileCommandRequest, IResult>
{
    [ValidationAspect(typeof(DeleteProfileValidator), Priority = 1)]
    public async Task<IResult> Handle(DeleteProfileCommandRequest request, CancellationToken cancellationToken)
    {
        var currentAccountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x =>
                x.Id == currentAccountId && x.AccountStatus == AccountStatus.Active) ??
            await accountRepository.GetAsync(x =>
                x.Username == request.Username && x.AccountStatus == AccountStatus.Active);

        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var isPasswordMatch = BCrypt.Net.BCrypt.Verify(request.Password, account.Password);
        if (!isPasswordMatch)
            throw new ApplicationException(Messages.PasswordError);


        await accountRepository.DeleteAsync(account.Id);
        return new SuccessResult();
    }
}