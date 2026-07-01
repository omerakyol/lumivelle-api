using Business.Helpers;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Notification.Commands.GetNotification;

public class GetNotificationCommandRequest : IRequest<IDataResult<TicketDeliveryStatus>>
{
    public string TicketId { get; set; }
}