using Business.Handlers.Accounts.Commands.RegisterFirebaseToken;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class RegisterFirebaseTokenValidator : AbstractValidator<RegisterFirebaseTokenCommandRequest>
{
    public RegisterFirebaseTokenValidator()
    {
        RuleFor(m => m.FirebaseToken).NotEmpty().WithMessage(Messages.DeviceTokenEmpty);
    }
}