using System.Collections.Generic;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Analysis.Queries.GetHistory;

public class GetHistoryQueryRequest : IRequest<IDataResult<List<BeautyProfileResult>>>
{
}
