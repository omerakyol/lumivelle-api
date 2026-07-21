using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetSavedPosts;

public class GetSavedPostsQueryRequest : IRequest<IDataResult<FeedPageResult>>
{
    public string Cursor { get; set; }
}
