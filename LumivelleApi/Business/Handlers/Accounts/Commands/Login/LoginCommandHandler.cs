using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Entities.Dtos.Account;
using Core.Enums;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace Business.Handlers.Accounts.Commands.Login;

public class LoginCommandHandler(
    IAccountRepository accountRepository,
    IRefreshTokenRepository refreshTokenRepository,
    ITokenHelper tokenHelper,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<LoginCommandRequest, IDataResult<LoginCommandResult>>
{
    [ValidationAspect(typeof(LoginValidator), Priority = 1)]
    public async Task<IDataResult<LoginCommandResult>> Handle(LoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        var account =
            await accountRepository.GetAsync(x =>
                x.Email == request.Email && x.AccountStatus == AccountStatus.Active);

        // Use one generic message for both "no account" and "wrong password" to
        // avoid leaking which usernames exist (account enumeration).
        if (account == null)
            throw new ApplicationException(Messages.InvalidCredentials);

        // Accounts created via Apple/Google sign-in may have no password set yet.
        var isPasswordMatch = !string.IsNullOrEmpty(account!.Password) &&
                               BCrypt.Net.BCrypt.Verify(request.Password, account.Password);
        if (!isPasswordMatch)
            throw new ApplicationException(Messages.InvalidCredentials);


        if (account!.TwoFactorEnabled)
            return string.IsNullOrWhiteSpace(account.TwoFactorSecretKey)
                ? throw new ApplicationException(Messages.TwoFactorNotSetup)
                : throw new ApplicationException(Messages.TwoFactorVerifyRequired);


        var accountDto = account.Adapt<AccountProfileDto>();

        var accessToken = tokenHelper.CreateToken<AccessToken>(account);

        Expression<Func<Core.Entities.Concrete.RefreshToken, bool>> oldRefreshTokensFilter =
            x => x.AccountId == account.Id && !x.IsRevoked && x.ExpiredAt > DateTime.UtcNow;
        var oldRefreshTokensUpdate = Builders<Core.Entities.Concrete.RefreshToken>.Update.Set(p => p.IsRevoked, true);
        await refreshTokenRepository.UpdateManyAsync(oldRefreshTokensFilter, oldRefreshTokensUpdate);

        await refreshTokenRepository.AddAsync(new Core.Entities.Concrete.RefreshToken
        {
            AccountId = account.Id,
            Token = accessToken.RefreshToken,
            ExpiredAt = DateTime.UtcNow.AddDays(1)
        });

        if (!string.IsNullOrEmpty(accountDto.PhotoUrl))
        {
            var contextRequest = httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{contextRequest?.Scheme}://{contextRequest?.Host.Value}";
            var fileUrl = $"{baseUrl}/media/{account.PhotoUrl}";
            accountDto.PhotoUrl = fileUrl;
        }

        var data = new LoginCommandResult
        {
            AccessToken = accessToken,
            Account = accountDto
        };
        return new SuccessDataResult<LoginCommandResult>(data);
    }
}