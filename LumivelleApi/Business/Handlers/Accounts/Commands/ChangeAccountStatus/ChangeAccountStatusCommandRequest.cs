using Core.Enums;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.ChangeAccountStatus;

public class ChangeAccountStatusCommandRequest : IRequest<IResult>
{
    public string AccountId { get; set; }
    public AccountStatus Status { get; set; }
}