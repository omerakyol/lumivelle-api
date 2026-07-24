using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Outfits.Commands.DeleteOutfit;

public class DeleteOutfitCommandHandler(IOutfitRepository outfitRepository)
    : IRequestHandler<DeleteOutfitCommandRequest, IResult>
{
    public async Task<IResult> Handle(DeleteOutfitCommandRequest request, CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var outfitId = ObjectId.Parse(request.Id);
        var document = await outfitRepository.GetByIdAsync(outfitId);

        if (document == null || document.AccountId != accountId)
            throw new ApplicationException(Messages.OutfitNotFound);

        await outfitRepository.DeleteAsync(outfitId);

        return new SuccessResult();
    }
}
