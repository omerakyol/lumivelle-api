using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Comments.Commands.DeleteComment;

public class DeleteCommentCommandRequest : IRequest<IResult>
{
    public string Id { get; set; }
}
