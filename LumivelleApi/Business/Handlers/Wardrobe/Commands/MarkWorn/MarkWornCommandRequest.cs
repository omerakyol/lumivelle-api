using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Commands.MarkWorn;

public class MarkWornCommandRequest : IRequest<IDataResult<WardrobeItemResult>>
{
    public string Id { get; set; }
}
