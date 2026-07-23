using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeAnalytics;

public class GetWardrobeAnalyticsQueryRequest : IRequest<IDataResult<WardrobeAnalyticsResult>>
{
}
