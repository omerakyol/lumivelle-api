using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Business.Handlers.Follows.Commands.ToggleFollow;

public class ToggleFollowCommandHandler(IFollowRepository followRepository, IAccountRepository accountRepository)
    : IRequestHandler<ToggleFollowCommandRequest, IDataResult<ToggleFollowResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<ToggleFollowResult>> Handle(
        ToggleFollowCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var followeeId = ObjectId.Parse(request.FolloweeId);

        if (followeeId == accountId)
            throw new ApplicationException(Messages.CannotFollowSelf);

        var existing = await followRepository.GetAsync(accountId, followeeId);
        bool isFollowing;

        if (existing != null)
        {
            await followRepository.DeleteAsync(existing.Id, softDelete: false);
            isFollowing = false;
        }
        else
        {
            var followee = await accountRepository.GetByIdAsync(followeeId);
            if (followee == null)
                throw new ApplicationException(Messages.AccountNotFound);

            try
            {
                await followRepository.AddAsync(new FollowDocument { FollowerId = accountId, FolloweeId = followeeId });
                isFollowing = true;
            }
            catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
            {
                isFollowing = true;
            }
        }

        var followerCount = await followRepository.CountFollowersAsync(followeeId);

        return new SuccessDataResult<ToggleFollowResult>(
            new ToggleFollowResult { IsFollowedByMe = isFollowing, FollowerCount = followerCount });
    }
}
