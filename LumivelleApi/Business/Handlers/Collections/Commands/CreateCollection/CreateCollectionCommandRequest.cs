using Business.Handlers.Collections;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Collections.Commands.CreateCollection;

public class CreateCollectionCommandRequest : IRequest<IDataResult<CollectionResult>>
{
    public string Name { get; set; }
}
