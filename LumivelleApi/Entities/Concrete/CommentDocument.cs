using Core.Entities;
using MongoDB.Bson;

namespace Entities.Concrete;

public class CommentDocument : DocumentDbEntity
{
    public ObjectId PostId { get; set; }
    public ObjectId AccountId { get; set; }
    public string Text { get; set; }
}
