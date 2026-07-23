using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeStats;

public class GetWardrobeStatsQueryRequest : IRequest<IDataResult<WardrobeStatsResult>>
{
}
