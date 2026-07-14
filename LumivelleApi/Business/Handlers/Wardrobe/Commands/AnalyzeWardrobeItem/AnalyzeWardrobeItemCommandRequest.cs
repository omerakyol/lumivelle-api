using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Commands.AnalyzeWardrobeItem;

public class AnalyzeWardrobeItemCommandRequest : IRequest<IDataResult<AnalyzeWardrobeItemResult>>
{
    public string ImageUrl { get; set; }
}
