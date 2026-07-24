using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Collections.Commands.MoveSavedPost;

public class MoveSavedPostCommandRequest : IRequest<IResult>
{
    public string PostId { get; set; }
    public string CollectionId { get; set; }
}
