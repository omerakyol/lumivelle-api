using System.Threading;
using System.Threading.Tasks;
using Business.Helpers;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Notification.Commands.CancelNotification;

public class CancelNotificationCommandHandler(
    IFirebaseNotificationService notificationService)
    : IRequestHandler<CancelNotificationCommandRequest, IResult>
{
    public async Task<IResult> Handle(CancelNotificationCommandRequest request,
        CancellationToken cancellationToken)
    {
        await notificationService.CancelNotification(request.Tokens, request.CollapseId);
        return new SuccessResult();
    }
}