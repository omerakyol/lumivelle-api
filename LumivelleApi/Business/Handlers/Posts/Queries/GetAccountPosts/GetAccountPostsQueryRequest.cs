using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetAccountPosts;

public class GetAccountPostsQueryRequest : IRequest<IDataResult<FeedPageResult>>
{
    public string AccountId { get; set; }
    public string Cursor { get; set; }
}
