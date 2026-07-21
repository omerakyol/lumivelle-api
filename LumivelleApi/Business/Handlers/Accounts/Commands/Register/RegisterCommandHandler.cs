using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Account;
using Core.Enums;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Mapster;
using MediatR;

namespace Business.Handlers.Accounts.Commands.Register;

public class RegisterCommandHandler(
    IAccountRepository accountRepository,
    ITokenHelper tokenHelper)
    : IRequestHandler<RegisterCommandRequest, IResult>
{
    [TransactionScopeAspect]
    [ValidationAspect(typeof(RegisterAccountValidator), Priority = 1)]
    public async Task<IResult> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var hasAlreadyAccount =
            accountRepository.Any(x => x.Email == request.Email, true);
        if (hasAlreadyAccount)
            throw new ApplicationException(Messages.AccountAlreadyHave);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var account = new Account
        {
            AccountType = AccountType.User,
            Email = request.Email,
            AccountStatus = AccountStatus.Active,
            Password = hashedPassword,
            FirebaseToken = request.FirebaseToken
        };

        await accountRepository.AddAsync(account);

        var accessToken = tokenHelper.CreateToken<AccessToken>(account);

        return new SuccessDataResult<RegisterCommandResult>
        {
            Success = true,
            Data = new RegisterCommandResult
            {
                AccessToken = accessToken,
                Account = account.Adapt<AccountDto>(),
            }
        };
    }
}