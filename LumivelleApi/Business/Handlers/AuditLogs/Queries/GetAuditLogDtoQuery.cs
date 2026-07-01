using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Caching;

using Core.Aspects.Autofac.Performance;

using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Newtonsoft.Json;

namespace Business.Handlers.AuditLogs.Queries;

public class GetAuditLogDtoQuery : IRequest<IDataResult<IEnumerable<LogDto>>>
{
    public class GetLogDtoQueryHandler(ILogRepository logRepository)
        : IRequestHandler<GetAuditLogDtoQuery, IDataResult<IEnumerable<LogDto>>>
    {
        [PerformanceAspect(5)]
        [CacheAspect(10)]
        public async Task<IDataResult<IEnumerable<LogDto>>> Handle(GetAuditLogDtoQuery request,
            CancellationToken cancellationToken)
        {
            var result = await logRepository.GetListAsync();
            var data = new List<LogDto>();
            foreach (var item in result)
            {
                var jsonMessage = JsonConvert.DeserializeObject<LogDto>(item.MessageTemplate);
                dynamic msg = JsonConvert.DeserializeObject(item.MessageTemplate);
                var valueList = msg.Parameters[0];
                var exceptionMessage = msg.ExceptionMessage;
                valueList = valueList.Value.ToString();

                var list = new LogDto
                {
                    Id = item.Id,
                    Level = item.Level,
                    TimeStamp = item.TimeStamp,
                    Type = msg.Parameters[0].Type,
                    User = jsonMessage.User,
                    Value = valueList,
                    ExceptionMessage = exceptionMessage
                };

                data.Add(list);
            }

            return new SuccessDataResult<IEnumerable<LogDto>>(data);
        }
    }
}