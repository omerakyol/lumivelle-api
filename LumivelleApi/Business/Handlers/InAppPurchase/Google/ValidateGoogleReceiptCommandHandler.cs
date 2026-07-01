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
using Google.Apis.AndroidPublisher.v3.Data;
using MediatR;


namespace Business.Handlers.InAppPurchase.Google;

public class ValidateGoogleReceiptCommandRequest : IRequest<IResult>
{
    public string PackageName { get; set; }
    public string ProductId { get; set; }
    public string PurchaseToken { get; set; }
    public string Username { get; set; }
    public string DeviceId { get; set; }
}

public class ValidateGoogleReceiptValidator : AbstractValidator<ValidateGoogleReceiptCommandRequest>
{
    public ValidateGoogleReceiptValidator()
    {
        RuleFor(m => m.PackageName).NotEmpty().WithMessage(Messages.PackageNameEmpty);
        RuleFor(m => m.ProductId).NotEmpty().WithMessage(Messages.ProductIdEmpty);
        RuleFor(m => m.PurchaseToken).NotEmpty().WithMessage(Messages.PurchaseTokenEmpty);
        RuleFor(m => m.Username).NotEmpty().WithMessage(Messages.UsernameEmpty);
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
            x.Username == request.Username && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var result = await googlePlayValidator.ValidatePurchaseAsync(
            request.PackageName,
            request.ProductId,
            request.PurchaseToken
        );

        if (result == null)
            throw new ApplicationException(Messages.ReceiptValidationFailed);

        return new SuccessDataResult<ProductPurchase>(result);
    }
}