using Business.Handlers.Posts;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetStyleCategory;

public class GetStyleCategoryQueryRequest : IRequest<IDataResult<StyleCategoryPageResult>>
{
    public string StyleTag { get; set; }
    public string SecondaryTag { get; set; }
    public string Cursor { get; set; }
}
