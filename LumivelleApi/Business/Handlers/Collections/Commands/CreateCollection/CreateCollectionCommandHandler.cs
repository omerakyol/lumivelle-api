using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Collections.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Collections.Commands.CreateCollection;

public class CreateCollectionCommandHandler(ICollectionRepository collectionRepository)
    : IRequestHandler<CreateCollectionCommandRequest, IDataResult<CollectionResult>>
{
    [SecuredOperation(Priority = 1)]
    [ValidationAspect(typeof(CreateCollectionValidator), Priority = 2)]
    public async Task<IDataResult<CollectionResult>> Handle(
        CreateCollectionCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();

        var document = new CollectionDocument
        {
            AccountId = accountId,
            Name = request.Name
        };

        await collectionRepository.AddAsync(document);

        return new SuccessDataResult<CollectionResult>(CollectionResult.FromDocument(document, 0, []));
    }
}
