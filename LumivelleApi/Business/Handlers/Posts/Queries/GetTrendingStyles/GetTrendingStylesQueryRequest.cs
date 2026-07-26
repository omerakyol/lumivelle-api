using System.Collections.Generic;
using Business.Handlers.Posts;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Queries.GetTrendingStyles;

public class GetTrendingStylesQueryRequest : IRequest<IDataResult<List<TrendResult>>>
{
    public string Range { get; set; }
}
