using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class PostLikeDocument : DocumentDbEntity
{
    public ObjectId PostId { get; set; }
    public ObjectId AccountId { get; set; }
}