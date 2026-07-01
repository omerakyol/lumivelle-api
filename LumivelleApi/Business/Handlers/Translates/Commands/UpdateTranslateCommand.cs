using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Translates.ValidationRules;
using Core.Aspects.Autofac.Caching;

using Core.Aspects.Autofac.Validation;
using Core.Constants;

using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Translates.Commands;

public class UpdateTranslateCommand : IRequest<IResult>
{
    public string Id { get; set; }
    public string LanguageCode { get; set; }
    public string Value { get; set; }
    public string Code { get; set; }

    public class UpdateTranslateCommandHandler(ITranslateRepository translateRepository)
        : IRequestHandler<UpdateTranslateCommand, IResult>
    {
        [AdminOperation(Priority = 1)]
        [ValidationAspect(typeof(UpdateTranslateValidator), Priority = 2)]
        [CacheRemoveAspect]
        public async Task<IResult> Handle(UpdateTranslateCommand request, CancellationToken cancellationToken)
        {
            var translateId = ObjectId.Parse(request.Id);
            var translate = await translateRepository.GetByIdAsync(translateId);
            if (translate == null)
                throw new ApplicationException(Messages.TranslateNotFound);

            translate.Language = request.LanguageCode;
            translate.Value = request.Value;
            translate.Code = request.Code;

            await translateRepository.UpdateAsync(translate);
            return new SuccessResult(new ResultMessage { Code = Messages.Updated, Description = Messages.Updated });
        }
    }
}