using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Commands.DeletePost;

public class DeletePostCommandRequest : IRequest<IResult>
{
    public string Id { get; set; }
}
