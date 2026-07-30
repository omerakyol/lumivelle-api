using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class PostDocument : DocumentDbEntity
{
    public ObjectId AccountId { get; set; }
    public string[] ImageUrls { get; set; } = [];
    public string Caption { get; set; } = string.Empty;
    public ObjectId? WardrobeItemId { get; set; }
    public ObjectId? OutfitId { get; set; }
    public int LikeCount { get; set; }
    public int SaveCount { get; set; }
    public int CommentCount { get; set; }
}