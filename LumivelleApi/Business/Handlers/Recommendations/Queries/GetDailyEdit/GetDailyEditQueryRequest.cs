using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Recommendations.Queries.GetDailyEdit;

public class GetDailyEditQueryRequest : IRequest<IDataResult<DailyEditResult>>
{
}
