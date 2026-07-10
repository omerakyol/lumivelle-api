using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Analysis.Queries.GetProfile;

public class GetProfileQueryRequest : IRequest<IDataResult<BeautyProfileResult>>
{
}
