using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;

using Core.Constants;

using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Translates.Commands;

public class DeleteTranslateCommand : IRequest<IResult>
{
    public string Id { get; set; }


    public class DeleteTranslateCommandHandler(ITranslateRepository translateRepository)
        : IRequestHandler<DeleteTranslateCommand, IResult>
    {
        [AdminOperation(Priority = 1)]
        [CacheRemoveAspect]
        public async Task<IResult> Handle(DeleteTranslateCommand request, CancellationToken cancellationToken)
        {
            var translateId = ObjectId.Parse(request.Id);
            await translateRepository.DeleteAsync(translateId);
            return new SuccessResult(new ResultMessage { Code = Messages.Deleted, Description = Messages.Deleted });
        }
    }
}