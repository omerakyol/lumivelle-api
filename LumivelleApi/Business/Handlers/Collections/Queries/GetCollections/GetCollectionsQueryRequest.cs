using System.Collections.Generic;
using Business.Handlers.Collections;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Collections.Queries.GetCollections;

public class GetCollectionsQueryRequest : IRequest<IDataResult<List<CollectionResult>>>
{
}
