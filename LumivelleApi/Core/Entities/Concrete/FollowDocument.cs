using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class FollowDocument : DocumentDbEntity
{
    public ObjectId FollowerId { get; set; }
    public ObjectId FolloweeId { get; set; }
}