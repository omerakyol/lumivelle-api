using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Analysis;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Analysis.Queries.GetProfile;

public class GetProfileQueryHandler(IBeautyProfileRepository beautyProfileRepository)
    : IRequestHandler<GetProfileQueryRequest, IDataResult<BeautyProfileResult>>
{
    [SecuredOperation(Priority = 1)]
    public async Task<IDataResult<BeautyProfileResult>> Handle(
        GetProfileQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var profile = await beautyProfileRepository.GetLatestByAccountIdAsync(accountId);

        if (profile == null)
            return new ErrorDataResult<BeautyProfileResult>(
                new ResultMessage
                {
                    Code = "NOT_FOUND",
                    Description = "Beauty profile not found"
                });

        return new SuccessDataResult<BeautyProfileResult>(BeautyProfileResult.FromDocument(profile));
    }
}
