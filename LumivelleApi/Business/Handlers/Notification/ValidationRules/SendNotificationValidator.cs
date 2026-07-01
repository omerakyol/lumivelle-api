using Business.Handlers.Notification.Commands.SendNotification;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Notification.ValidationRules;

public class SendNotificationValidator : AbstractValidator<SendNotificationCommandRequest>
{
    public SendNotificationValidator()
    {
        RuleFor(m => m.Title).NotEmpty().WithMessage(Messages.NotificationTitleEmpty);
        RuleFor(m => m.Body).NotEmpty().WithMessage(Messages.NotificationContentEmpty);
        RuleFor(m => m.Tokens).NotEqual([]).WithMessage(Messages.NotificationDeviceTokensEmpty);
    }
}