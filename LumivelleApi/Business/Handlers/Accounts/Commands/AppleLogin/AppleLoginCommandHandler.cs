using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.Commands.Login;
using Business.Handlers.Accounts.ValidationRules;
using Business.Helpers;
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
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace Business.Handlers.Accounts.Commands.AppleLogin;

public class AppleLoginCommandHandler(
    IAccountRepository accountRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IAppleSignInVerifier appleSignInVerifier,
    ITokenHelper tokenHelper,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<AppleLoginCommandRequest, IDataResult<LoginCommandResult>>
{
    [ValidationAspect(typeof(AppleLoginValidator), Priority = 1)]
    public async Task<IDataResult<LoginCommandResult>> Handle(AppleLoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        var identity = await appleSignInVerifier.VerifyAsync(request.IdentityToken);
        if (identity == null)
            throw new ApplicationException(Messages.SocialTokenInvalid);

        var account = await accountRepository.GetAsync(x =>
            x.AppleUserId == identity.Sub && x.AccountStatus == AccountStatus.Active);

        if (account == null && !string.IsNullOrEmpty(identity.Email))
        {
            // Apple/Google both attest the email is verified, so an existing password account
            // with the same email is treated as the same person and linked automatically.
            var existingByEmail = await accountRepository.GetAsync(x =>
                x.Email == identity.Email && x.AccountStatus == AccountStatus.Active);
            if (existingByEmail != null)
            {
                existingByEmail.AppleUserId = identity.Sub;
                await accountRepository.UpdateAsync(existingByEmail, x => x.Id == existingByEmail.Id);
                account = existingByEmail;
            }
        }

        if (account == null)
        {
            // Apple only includes an email on the very first authorization for a given app; on
            // later logins the identity token still carries it, but if the user chose "Hide My
            // Email" the value is a stable per-app relay address — still usable as the account's
            // login email.
            if (string.IsNullOrEmpty(identity.Email))
                throw new ApplicationException(Messages.SocialTokenInvalid);

            account = new Account
            {
                AccountType = AccountType.User,
                AccountStatus = AccountStatus.Active,
                Email = identity.Email,
                AppleUserId = identity.Sub
            };
            await accountRepository.AddAsync(account);
        }

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
            accountDto.PhotoUrl = $"{baseUrl}/media/{account.PhotoUrl}";
        }

        var data = new LoginCommandResult
        {
            AccessToken = accessToken,
            Account = accountDto
        };
        return new SuccessDataResult<LoginCommandResult>(data);
    }
}
