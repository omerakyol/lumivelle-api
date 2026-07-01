using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.ResetTwoFactor;

public class ResetTwoFactorCommandRequest : IRequest<IResult>
{
    public string AccountId { get; set; }
}