using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Shades.Queries.GetShadesByCategory;

public class GetShadesByCategoryQueryHandler(IShadeRepository shadeRepository)
    : IRequestHandler<GetShadesByCategoryQueryRequest, IDataResult<List<ShadeResult>>>
{
    public async Task<IDataResult<List<ShadeResult>>> Handle(
        GetShadesByCategoryQueryRequest request,
        CancellationToken cancellationToken)
    {
        var documents = await shadeRepository.GetByCategoryAsync(request.Category);

        var results = documents
            .Select(d => new ShadeResult { Id = d.Id.ToString(), Name = d.Name, Hex = d.Hex })
            .ToList();

        return new SuccessDataResult<List<ShadeResult>>(results);
    }
}