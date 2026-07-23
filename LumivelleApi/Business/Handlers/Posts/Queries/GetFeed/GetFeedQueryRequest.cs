using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetFeed;

public class GetFeedQueryRequest : IRequest<IDataResult<FeedPageResult>>
{
    public string Cursor { get; set; }
    public string Category { get; set; }
}
