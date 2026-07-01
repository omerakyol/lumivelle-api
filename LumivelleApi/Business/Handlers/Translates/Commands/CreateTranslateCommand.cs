using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Translates.ValidationRules;
using Core.Aspects.Autofac.Caching;

using Core.Aspects.Autofac.Validation;
using Core.Constants;

using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Translates.Commands;

public class CreateTranslateCommand : IRequest<IResult>
{
    public string LanguageCode { get; set; }
    public string Value { get; set; }
    public string Code { get; set; }


    public class CreateTranslateCommandHandler(ITranslateRepository translateRepository)
        : IRequestHandler<CreateTranslateCommand, IResult>
    {
        [AdminOperation(Priority = 1)]
        [ValidationAspect(typeof(CreateTranslateValidator), Priority = 2)]
        [CacheRemoveAspect]
        public async Task<IResult> Handle(CreateTranslateCommand request, CancellationToken cancellationToken)
        {
            var isThereTranslateRecord = translateRepository
                .Any(u => u.Language == request.LanguageCode && u.Code == request.Code);

            if (isThereTranslateRecord)
                throw new ApplicationException(Messages.NameAlreadyExist);

            var addedTranslate = new Translate
            {
                Language = request.LanguageCode,
                Value = request.Value,
                Code = request.Code
            };

            await translateRepository.AddAsync(addedTranslate);
            return new SuccessResult(new ResultMessage { Code = Messages.Added, Description = Messages.Added });
        }
    }
}