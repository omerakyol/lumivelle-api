using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Analysis.Commands.Analyze;

public class AnalyzeCommandRequest : IRequest<IDataResult<BeautyProfileResult>>
{
    public string ImageUrl { get; set; }
}
