using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Commands.UpdateWardrobeItem;

public class UpdateWardrobeItemCommandRequest : IRequest<IDataResult<WardrobeItemResult>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string[] Colors { get; set; } = [];
    public string[] StyleTags { get; set; } = [];
}
