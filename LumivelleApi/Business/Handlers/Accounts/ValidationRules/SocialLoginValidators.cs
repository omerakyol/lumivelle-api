using Business.Handlers.Accounts.Commands.AppleLogin;
using Business.Handlers.Accounts.Commands.GoogleLogin;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class GoogleLoginValidator : AbstractValidator<GoogleLoginCommandRequest>
{
    public GoogleLoginValidator()
    {
        RuleFor(m => m.IdToken).NotEmpty().WithMessage(Messages.SocialTokenEmpty);
        RuleFor(m => m.DeviceId).NotEmpty().WithMessage(Messages.DeviceIdEmpty);
    }
}

public class AppleLoginValidator : AbstractValidator<AppleLoginCommandRequest>
{
    public AppleLoginValidator()
    {
        RuleFor(m => m.IdentityToken).NotEmpty().WithMessage(Messages.SocialTokenEmpty);
        RuleFor(m => m.DeviceId).NotEmpty().WithMessage(Messages.DeviceIdEmpty);
    }
}
