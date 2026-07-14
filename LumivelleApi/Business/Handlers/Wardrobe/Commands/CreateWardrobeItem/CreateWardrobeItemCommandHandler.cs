using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Wardrobe.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Wardrobe.Commands.CreateWardrobeItem;

public class CreateWardrobeItemCommandHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IBeautyProfileRepository beautyProfileRepository)
    : IRequestHandler<CreateWardrobeItemCommandRequest, IDataResult<WardrobeItemResult>>
{
    [SecuredOperation(Priority = 1)]
    [ValidationAspect(typeof(CreateWardrobeItemValidator), Priority = 2)]
    public async Task<IDataResult<WardrobeItemResult>> Handle(
        CreateWardrobeItemCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var profile = await beautyProfileRepository.GetLatestByAccountIdAsync(accountId);
        var palette = profile?.Palette ?? [];

        var document = new WardrobeItemDocument
        {
            AccountId = accountId,
            Name = request.Name,
            Category = request.Category,
            Colors = request.Colors,
            StyleTags = request.StyleTags,
            ImageUrl = request.ImageUrl,
            PaletteMatchScore = PaletteMatching.ScoreColorsAgainstPalette(request.Colors, palette)
        };

        await wardrobeItemRepository.AddAsync(document);

        return new SuccessDataResult<WardrobeItemResult>(WardrobeItemResult.FromDocument(document));
    }
}
