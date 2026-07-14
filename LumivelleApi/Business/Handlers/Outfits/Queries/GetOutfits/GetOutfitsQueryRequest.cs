using System.Collections.Generic;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Outfits.Queries.GetOutfits;

public class GetOutfitsQueryRequest : IRequest<IDataResult<List<OutfitResult>>>
{
}
