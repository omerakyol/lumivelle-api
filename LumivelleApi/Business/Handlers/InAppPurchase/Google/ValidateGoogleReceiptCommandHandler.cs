using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Helpers;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Enums;
using Core.Utilities.Results;
using DataAccess.Abstract;
using FluentValidation;
using MediatR;


namespace Business.Handlers.InAppPurchase.Google;

public class ValidateGoogleReceiptCommandRequest : IRequest<IResult>
{
    public string PackageName { get; set; }
    public string ProductId { get; set; }
    public string PurchaseToken { get; set; }
    public string Email { get; set; }
    public string DeviceId { get; set; }
}

public class ValidateGoogleReceiptValidator : AbstractValidator<ValidateGoogleReceiptCommandRequest>
{
    public ValidateGoogleReceiptValidator()
    {
        RuleFor(m => m.PackageName).NotEmpty().WithMessage(Messages.PackageNameEmpty);
        RuleFor(m => m.ProductId).NotEmpty().WithMessage(Messages.ProductIdEmpty);
        RuleFor(m => m.PurchaseToken).NotEmpty().WithMessage(Messages.PurchaseTokenEmpty);
        RuleFor(m => m.Email).EmailAddress().WithMessage(Messages.InvalidEmail);
        RuleFor(m => m.DeviceId).NotEmpty().WithMessage(Messages.DeviceIdEmpty);
    }
}

public class ValidateGoogleReceiptCommandHandler(
    IGooglePlayValidator googlePlayValidator,
    IAccountRepository accountRepository,
    IMediator mediator)
    : IRequestHandler<ValidateGoogleReceiptCommandRequest, IResult>
{
    [ValidationAspect(typeof(ValidateGoogleReceiptValidator), Priority = 1)]
    public async Task<IResult> Handle(ValidateGoogleReceiptCommandRequest request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetAsync(x =>
            x.Email == request.Email && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var subscription = await googlePlayValidator.ValidateSubscriptionAsync(request.PackageName, request.PurchaseToken);
        if (subscription == null)
            throw new ApplicationException(Messages.ReceiptValidationFailed);

        account.SubscriptionTier = subscription.IsActive ? SubscriptionTier.Premium : SubscriptionTier.Free;
        account.SubscriptionPlatform = "google";
        account.SubscriptionProductId = subscription.ProductId;
        account.SubscriptionExpiresAt = subscription.ExpiresAt;
        account.SubscriptionAutoRenewing = subscription.AutoRenewing;

        await accountRepository.UpdateAsync(account, x => x.Id == account.Id);

        return new SuccessDataResult<GoogleSubscriptionPurchase>(subscription);
    }
}