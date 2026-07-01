using Core.Entities.Concrete;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.UpdateDeviceInformation;

public class UpdateDeviceInformationCommandRequest : IRequest<IResult>
{
    public DeviceInformation DeviceInformation { get; set; }
}