using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Collections.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Collections.Commands.CreateCollection;

public class CreateCollectionCommandHandler(
    ICollectionRepository collectionRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<CreateCollectionCommandRequest, IDataResult<CollectionResult>>
{
    [ValidationAspect(typeof(CreateCollectionValidator), Priority = 2)]
    public async Task<IDataResult<CollectionResult>> Handle(
        CreateCollectionCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var document = new CollectionDocument
        {
            AccountId = accountId,
            Name = request.Name
        };

        await collectionRepository.AddAsync(document);

        return new SuccessDataResult<CollectionResult>(CollectionResult.FromDocument(document, 0, []));
    }
}