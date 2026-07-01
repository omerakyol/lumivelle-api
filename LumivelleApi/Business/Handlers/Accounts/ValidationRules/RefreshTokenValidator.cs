using Business.Handlers.Accounts.Commands.RefreshToken;
using Core.Constants;
using FluentValidation;

namespace Business.Handlers.Accounts.ValidationRules;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommandRequest>
{
    public RefreshTokenValidator()
    {
        RuleFor(m => m.AccessToken).NotEmpty().WithMessage(Messages.TokenEmpty);
        RuleFor(m => m.RefreshToken).NotEmpty().WithMessage(Messages.TokenEmpty);
    }
}