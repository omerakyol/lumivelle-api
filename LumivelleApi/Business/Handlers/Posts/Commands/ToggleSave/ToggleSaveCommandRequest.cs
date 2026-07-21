using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Commands.ToggleSave;

public class ToggleSaveCommandRequest : IRequest<IDataResult<ToggleSaveResult>>
{
    public string PostId { get; set; }
}
