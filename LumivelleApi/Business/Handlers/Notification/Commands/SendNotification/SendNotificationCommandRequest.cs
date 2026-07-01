using Business.Helpers;
using Core.Utilities.Results;
using FirebaseAdmin.Messaging;
using MediatR;

namespace Business.Handlers.Notification.Commands.SendNotification;

public class SendNotificationCommandRequest: FirebasePushRequest, IRequest<IDataResult<BatchResponse>>
{ 
}