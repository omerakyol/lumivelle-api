using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Analysis;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Analysis.Queries.GetProfile;

public class GetProfileQueryHandler(IBeautyProfileRepository beautyProfileRepository)
    : IRequestHandler<GetProfileQueryRequest, IDataResult<BeautyProfileResult>>
{
    public async Task<IDataResult<BeautyProfileResult>> Handle(
        GetProfileQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var profile = await beautyProfileRepository.GetLatestByAccountIdAsync(accountId);

        if (profile == null)
            throw new ApplicationException(Messages.BeautyProfileNotFound);

        return new SuccessDataResult<BeautyProfileResult>(BeautyProfileResult.FromDocument(profile));
    }
}
