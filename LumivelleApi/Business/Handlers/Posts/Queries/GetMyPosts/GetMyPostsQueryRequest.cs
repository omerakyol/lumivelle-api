using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetMyPosts;

public class GetMyPostsQueryRequest : IRequest<IDataResult<FeedPageResult>>
{
    public string Cursor { get; set; }
}
