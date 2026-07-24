using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Wardrobe.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Wardrobe.Commands.UpdateWardrobeItem;

public class UpdateWardrobeItemCommandHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IBeautyProfileRepository beautyProfileRepository)
    : IRequestHandler<UpdateWardrobeItemCommandRequest, IDataResult<WardrobeItemResult>>
{
    [ValidationAspect(typeof(UpdateWardrobeItemValidator), Priority = 2)]
    public async Task<IDataResult<WardrobeItemResult>> Handle(
        UpdateWardrobeItemCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var itemId = ObjectId.Parse(request.Id);
        var document = await wardrobeItemRepository.GetByIdAsync(itemId);

        if (document == null || document.AccountId != accountId)
            throw new ApplicationException(Messages.WardrobeItemNotFound);

        var profile = await beautyProfileRepository.GetLatestByAccountIdAsync(accountId);
        var palette = profile?.Palette ?? [];

        document.Name = request.Name;
        document.Category = request.Category;
        document.Colors = request.Colors;
        document.StyleTags = request.StyleTags;
        document.PaletteMatchScore = PaletteMatching.ScoreColorsAgainstPalette(request.Colors, palette);

        await wardrobeItemRepository.UpdateAsync(document);

        return new SuccessDataResult<WardrobeItemResult>(WardrobeItemResult.FromDocument(document));
    }
}
