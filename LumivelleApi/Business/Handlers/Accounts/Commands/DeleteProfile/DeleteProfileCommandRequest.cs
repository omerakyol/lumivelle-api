using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Business.Handlers.Accounts.Commands.DeleteProfile;

public class DeleteProfileCommandRequest : IRequest<IResult>
{
    [FromQuery(Name = "username")]
    public string? Username { get; set; }
    [FromQuery(Name = "password")]
    public string Password { get; set; }
}