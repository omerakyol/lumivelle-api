using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeItems;

public class GetWardrobeItemsQueryHandler(IWardrobeItemRepository wardrobeItemRepository)
    : IRequestHandler<GetWardrobeItemsQueryRequest, IDataResult<List<WardrobeItemResult>>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<List<WardrobeItemResult>>> Handle(
        GetWardrobeItemsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var documents = await wardrobeItemRepository.GetByAccountIdAsync(accountId, request.Category);

        var results = documents.Select(WardrobeItemResult.FromDocument).ToList();

        return new SuccessDataResult<List<WardrobeItemResult>>(results);
    }
}
