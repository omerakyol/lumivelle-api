using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Comments.Queries.GetComments;

public class GetCommentsQueryRequest : IRequest<IDataResult<CommentPageResult>>
{
    public string PostId { get; set; }
    public string Cursor { get; set; }
}
