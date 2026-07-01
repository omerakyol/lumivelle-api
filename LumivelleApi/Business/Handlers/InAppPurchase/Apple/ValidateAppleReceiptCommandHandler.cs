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


namespace Business.Handlers.InAppPurchase.Apple;

public class ValidateAppleReceiptCommandRequest : IRequest<IResult>
{
    public string ReceiptData { get; set; }
    public string Username { get; set; }
    public string DeviceId { get; set; }
}

public class ValidateAppleReceiptValidator : AbstractValidator<ValidateAppleReceiptCommandRequest>
{
    public ValidateAppleReceiptValidator()
    {
        RuleFor(m => m.ReceiptData).NotEmpty().WithMessage(Messages.ReceiptDataEmpty);
        RuleFor(m => m.Username).NotEmpty().WithMessage(Messages.UsernameEmpty);
        RuleFor(m => m.DeviceId).NotEmpty().WithMessage(Messages.DeviceIdEmpty);
    }
}

public class ValidateAppleReceiptCommandHandler(
    IApplePurchaseValidator applePurchaseValidator,
    IAccountRepository accountRepository,
    IMediator mediator)
    : IRequestHandler<ValidateAppleReceiptCommandRequest, IResult>
{
    [ValidationAspect(typeof(ValidateAppleReceiptValidator), Priority = 1)]
    public async Task<IResult> Handle(ValidateAppleReceiptCommandRequest request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetAsync(x =>
            x.Username == request.Username && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var result = await applePurchaseValidator.ValidateAsync(request.ReceiptData);
        if (result == null)
            throw new ApplicationException(Messages.ReceiptValidationFailed);

        return new SuccessDataResult<AppleInAppPurchase>(result);
    }
}