using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Queries.GetAccountPublicProfile;

public class GetAccountPublicProfileQueryRequest : IRequest<IDataResult<AccountPublicProfileResult>>
{
    public string Id { get; set; }
}
