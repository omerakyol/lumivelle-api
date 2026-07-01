using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.RefreshToken;

public class RefreshTokenCommandRequest : IRequest<IResult>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}