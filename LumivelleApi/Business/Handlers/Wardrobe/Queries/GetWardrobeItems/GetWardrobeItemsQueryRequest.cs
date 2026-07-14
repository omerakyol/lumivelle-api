using System.Collections.Generic;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeItems;

public class GetWardrobeItemsQueryRequest : IRequest<IDataResult<List<WardrobeItemResult>>>
{
    public string Category { get; set; }
}
