using System.Threading;
using System.Threading.Tasks;
using Business.Helpers;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Notification.Commands.GetNotification;

public class GetNotificationCommandHandler(
    IExpoNotificationService notificationService)
    : IRequestHandler<GetNotificationCommandRequest, IDataResult<TicketDeliveryStatus>>
{
    
    public async Task<IDataResult<TicketDeliveryStatus>> Handle(GetNotificationCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await notificationService.GetNotificationReceiptsAsync(request.TicketId);
        return new SuccessDataResult<TicketDeliveryStatus>(response);
    }
}