using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Outfits.Commands.CreateOutfit;

public class CreateOutfitCommandRequest : IRequest<IDataResult<OutfitResult>>
{
    public string Name { get; set; }
    public string[] ItemIds { get; set; } = [];
}
