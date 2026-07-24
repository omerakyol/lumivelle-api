using Business.Handlers.Posts;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Collections.Queries.GetCollectionPosts;

public class GetCollectionPostsQueryRequest : IRequest<IDataResult<FeedPageResult>>
{
    public string CollectionId { get; set; }
    public string Cursor { get; set; }
}
