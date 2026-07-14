using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Outfits.Commands.DeleteOutfit;

public class DeleteOutfitCommandRequest : IRequest<IResult>
{
    public string Id { get; set; }
}
