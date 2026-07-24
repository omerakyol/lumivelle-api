using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.UpdateProfile;

public class UpdateProfileCommandRequest : IRequest<IResult>
{
    public string DisplayName { get; set; }
    public string Bio { get; set; }
}
