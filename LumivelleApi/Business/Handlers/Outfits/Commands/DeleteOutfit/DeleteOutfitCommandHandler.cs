using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Outfits.Commands.DeleteOutfit;

public class DeleteOutfitCommandHandler(
    IOutfitRepository outfitRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<DeleteOutfitCommandRequest, IResult>
{
    public async Task<IResult> Handle(DeleteOutfitCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var outfitId = ObjectId.Parse(request.Id);
        var document = await outfitRepository.GetByIdAsync(outfitId);

        if (document == null || document.AccountId != accountId)
            throw new ApplicationException(Messages.OutfitNotFound);

        await outfitRepository.DeleteAsync(outfitId);

        return new SuccessResult();
    }
}
