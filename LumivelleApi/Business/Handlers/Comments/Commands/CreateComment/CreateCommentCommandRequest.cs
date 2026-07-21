using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Comments.Commands.CreateComment;

public class CreateCommentCommandRequest : IRequest<IDataResult<CommentResult>>
{
    public string PostId { get; set; }
    public string Text { get; set; }
}
