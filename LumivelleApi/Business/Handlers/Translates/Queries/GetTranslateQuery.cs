using System.Threading;
using System.Threading.Tasks;
using Business.Helpers;


using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Translates.Queries;

public class GetTranslateQuery : IRequest<IDataResult<Translate>>
{
    public ObjectId? Id { get; set; }
    public string? Code { get; set; }
    public string? Language { get; set; }

    public class GetTranslateQueryHandler(ITranslateRepository translateRepository, ILanguageHelper languageHelper)
        : IRequestHandler<GetTranslateQuery, IDataResult<Translate>>
    {
        public async Task<IDataResult<Translate>> Handle(GetTranslateQuery request, CancellationToken cancellationToken)
        {
            Translate translate = null;
            if (request.Id.HasValue)
                translate = await translateRepository.GetAsync(p => p.Id == request.Id);
            else
            {
                var acceptLanguage = request.Language ?? languageHelper.GetAcceptLanguageWithDefaultRules();
                translate = await translateRepository.GetAsync(p =>
                    p.Code == request.Code && p.Language == acceptLanguage);
            }

            return new SuccessDataResult<Translate>(translate);
        }
    }
}