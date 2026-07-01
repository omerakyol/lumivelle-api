using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandRequest : IRequest<IResult>
{
    public string AccountId { get; set; }
}