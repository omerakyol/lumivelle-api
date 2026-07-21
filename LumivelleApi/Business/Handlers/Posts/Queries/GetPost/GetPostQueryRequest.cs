using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetPost;

public class GetPostQueryRequest : IRequest<IDataResult<PostResult>>
{
    public string Id { get; set; }
}
