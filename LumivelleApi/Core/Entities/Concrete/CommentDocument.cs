using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class CommentDocument : DocumentDbEntity
{
    public ObjectId PostId { get; set; }
    public ObjectId AccountId { get; set; }
    public string Text { get; set; }
}