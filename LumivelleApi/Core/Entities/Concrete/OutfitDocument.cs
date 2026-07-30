using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class OutfitDocument : DocumentDbEntity
{
    public ObjectId AccountId { get; set; }
    public string Name { get; set; }
    public ObjectId[] ItemIds { get; set; } = [];
}