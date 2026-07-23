using Core.Entities;
using MongoDB.Bson;

namespace Entities.Concrete;

public class CollectionDocument : DocumentDbEntity
{
    public ObjectId AccountId { get; set; }
    public string Name { get; set; }
}
