using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class CollectionDocument : DocumentDbEntity
{
    public ObjectId AccountId { get; set; }
    public string Name { get; set; }
}