using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Commands.ToggleLike;

public class ToggleLikeCommandRequest : IRequest<IDataResult<ToggleLikeResult>>
{
    public string PostId { get; set; }
}
