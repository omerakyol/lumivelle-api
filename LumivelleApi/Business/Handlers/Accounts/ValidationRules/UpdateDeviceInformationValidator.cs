using Business.Handlers.Accounts.Commands.UpdateDeviceInformation;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class UpdateDeviceInformationValidator : AbstractValidator<UpdateDeviceInformationCommandRequest>
{
    public UpdateDeviceInformationValidator()
    {
        RuleFor(m => m.DeviceInformation).NotEmpty().WithMessage(Messages.DeviceInformationEmpty);
    }
}