using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Follows.Commands.ToggleFollow;

public class ToggleFollowCommandRequest : IRequest<IDataResult<ToggleFollowResult>>
{
    public string FolloweeId { get; set; }
}
