using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class SavedPostDocument : DocumentDbEntity
{
    public ObjectId PostId { get; set; }
    public ObjectId AccountId { get; set; }
    public ObjectId? CollectionId { get; set; }
}