using Core.Entities;
using MongoDB.Bson;

namespace Entities.Concrete;

public class FollowDocument : DocumentDbEntity
{
    public ObjectId FollowerId { get; set; }
    public ObjectId FolloweeId { get; set; }
}
