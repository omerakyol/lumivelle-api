using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Analysis;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Analysis.Queries.GetHistory;

public class GetHistoryQueryHandler(IBeautyProfileRepository beautyProfileRepository)
    : IRequestHandler<GetHistoryQueryRequest, IDataResult<List<BeautyProfileResult>>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<List<BeautyProfileResult>>> Handle(
        GetHistoryQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var documents = await beautyProfileRepository.GetAllByAccountIdAsync(accountId);
        var results = documents.Select(BeautyProfileResult.FromDocument).ToList();
        return new SuccessDataResult<List<BeautyProfileResult>>(results);
    }
}
