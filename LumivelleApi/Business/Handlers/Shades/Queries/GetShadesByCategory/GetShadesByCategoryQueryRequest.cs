using System.Collections.Generic;
using Business.Handlers.Shades;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Shades.Queries.GetShadesByCategory;

public class GetShadesByCategoryQueryRequest : IRequest<IDataResult<List<ShadeResult>>>
{
    public string Category { get; set; }
}
