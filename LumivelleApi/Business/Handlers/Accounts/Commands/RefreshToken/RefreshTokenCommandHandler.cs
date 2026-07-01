using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Accounts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Accounts.Commands.RefreshToken;

public class LoginCommandHandler(
    IAccountRepository accountRepository,
    IRefreshTokenRepository refreshTokenRepository,
    ITokenHelper tokenHelper)
    : IRequestHandler<RefreshTokenCommandRequest, IResult>
{
    [ValidationAspect(typeof(RefreshTokenValidator), Priority = 1)]
    public async Task<IResult> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        var jwtToken = tokenHelper.DecodeToken(request.AccessToken);
        var accountIdFromToken = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimExtensions.NameIdentifier)?.Value;
        var accountId = ObjectId.Parse(accountIdFromToken);

        var account = await accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var refreshToken = await refreshTokenRepository.GetAsync(x =>
            x.AccountId == accountId && x.Token == request.RefreshToken && !x.IsRevoked &&
            x.ExpiredAt > DateTime.UtcNow);
        if (refreshToken == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var accessToken = tokenHelper.CreateToken<AccessToken>(account);

        refreshToken.IsRevoked = true;
        await refreshTokenRepository.UpdateAsync(refreshToken);

        await refreshTokenRepository.AddAsync(new Core.Entities.Concrete.RefreshToken
        {
            AccountId = accountId,
            Token = accessToken.RefreshToken,
            ExpiredAt = DateTime.UtcNow.AddDays(1)
        });

        return new SuccessDataResult<RefreshTokenCommandResult>(new RefreshTokenCommandResult
        {
            AccessToken = accessToken
        });
    }
}