using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.SetAccountFlags;

public class SetAccountFlagsCommandRequest : IRequest<IResult>
{
    public string Id { get; set; }
    public bool IsVerified { get; set; }
    public bool IsCreator { get; set; }
}
