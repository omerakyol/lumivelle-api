using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Enums;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler(
    IAccountRepository accountRepository)
    : IRequestHandler<CreateAccountCommandRequest, IDataResult<CreateAccountCommandResult>>
{
    [AdminOperation(Priority = 1)]
    [ValidationAspect(typeof(CreateAccountValidator), Priority = 2)]
    public async Task<IDataResult<CreateAccountCommandResult>> Handle(CreateAccountCommandRequest request,
        CancellationToken cancellationToken)
    {
        var hasAlreadyAccount =
            accountRepository.Any(x => x.Email == request.Email);
        if (hasAlreadyAccount)
            throw new ApplicationException(Messages.AccountAlreadyHave);


        request.AccountType ??= AccountType.User;

        if (request.AccountType == AccountType.User)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var account = new Account
            {
                AccountType = AccountType.User,
                Email = request.Email,
                AccountStatus = AccountStatus.Active,
                Password = hashedPassword
            };

            await accountRepository.AddAsync(account);


            return new SuccessDataResult<CreateAccountCommandResult>(new CreateAccountCommandResult
                { AccountId = account.Id.ToString() });
        }
        else
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var account = new Account
            {
                AccountType = AccountType.Admin,
                Email = request.Email,
                AccountStatus = AccountStatus.Active,
                Password = hashedPassword
            };

            await accountRepository.AddAsync(account);

            return new SuccessDataResult<CreateAccountCommandResult>(new CreateAccountCommandResult
                { AccountId = account.Id.ToString() });
        }
    }
}