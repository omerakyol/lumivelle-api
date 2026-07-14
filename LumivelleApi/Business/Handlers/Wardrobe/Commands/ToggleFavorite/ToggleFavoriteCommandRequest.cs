using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Commands.ToggleFavorite;

public class ToggleFavoriteCommandRequest : IRequest<IDataResult<WardrobeItemResult>>
{
    public string Id { get; set; }
}
