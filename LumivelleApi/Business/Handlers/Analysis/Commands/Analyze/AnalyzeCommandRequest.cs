using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Analysis.Commands.Analyze;

public class AnalyzeCommandRequest : IRequest<IDataResult<BeautyProfileResult>>
{
    public IFormFile File { get; set; } 
}