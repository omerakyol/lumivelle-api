using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Commands.CreateWardrobeItem;

public class CreateWardrobeItemCommandRequest : IRequest<IDataResult<WardrobeItemResult>>
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string[] Colors { get; set; } = [];
    public string[] StyleTags { get; set; } = [];
    public string ImageUrl { get; set; }
}
