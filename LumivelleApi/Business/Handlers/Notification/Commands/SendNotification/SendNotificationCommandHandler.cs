using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Notification.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using FirebaseAdmin.Messaging;
using MediatR;

namespace Business.Handlers.Notification.Commands.SendNotification;

public class SendNotificationCommandHandler(
    IFirebaseNotificationService notificationService)
    : IRequestHandler<SendNotificationCommandRequest, IDataResult<BatchResponse>>
{
    [ValidationAspect(typeof(SendNotificationValidator), Priority = 1)]
    public async Task<IDataResult<BatchResponse>> Handle(SendNotificationCommandRequest request,
        CancellationToken cancellationToken)
    {
        var response = await notificationService.SendNotificationAsync(request);
        return new SuccessDataResult<BatchResponse>(response);
    }
}