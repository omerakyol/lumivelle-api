using System.Collections.Generic;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Queries.GetPaletteGaps;

public class GetPaletteGapsQueryRequest : IRequest<IDataResult<List<PaletteGapResult>>>
{
}
