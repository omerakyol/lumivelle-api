using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Commands.DeleteWardrobeItem;

public class DeleteWardrobeItemCommandRequest : IRequest<IResult>
{
    public string Id { get; set; }
}
