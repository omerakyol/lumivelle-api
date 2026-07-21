using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Posts.Commands.CreatePost;

public class CreatePostCommandRequest : IRequest<IDataResult<PostResult>>
{
    public string[] ImageUrls { get; set; } = [];
    public string Caption { get; set; } = string.Empty;
    public string WardrobeItemId { get; set; }
    public string OutfitId { get; set; }
}
