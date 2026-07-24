using Business.Handlers.Follows;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Follows.Queries.GetCreatorProfile;

public class GetCreatorProfileQueryRequest : IRequest<IDataResult<CreatorProfileResult>>
{
    public string Id { get; set; }
}
