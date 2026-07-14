using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeItem;

public class GetWardrobeItemQueryRequest : IRequest<IDataResult<WardrobeItemResult>>
{
    public string Id { get; set; }
}
