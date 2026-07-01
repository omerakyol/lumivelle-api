using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;


using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Translates.Queries;

public class GetTranslatesQuery : IRequest<IDataResult<IEnumerable<Translate>>>
{
    public string? LanguageCode { get; set; } = null;

    public class
        GetTranslatesQueryHandler(ITranslateRepository translateRepository)
        : IRequestHandler<GetTranslatesQuery,
            IDataResult<IEnumerable<Translate>>>
    {
        [AdminOperation(Priority = 1)]
        [CacheAspect(60)]
        public async Task<IDataResult<IEnumerable<Translate>>> Handle(GetTranslatesQuery request,
            CancellationToken cancellationToken)
        {
            return new SuccessDataResult<IEnumerable<Translate>>(
                await translateRepository.GetTranslates(request.LanguageCode));
        }
    }
}