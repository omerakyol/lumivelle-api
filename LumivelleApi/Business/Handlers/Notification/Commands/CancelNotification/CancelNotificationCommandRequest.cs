using System.Collections.Generic;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Notification.Commands.CancelNotification;

public class CancelNotificationCommandRequest: IRequest<IResult>
{ 
    public List<string> Tokens { get; set; } = [];
    public string CollapseId { get; set; }
}