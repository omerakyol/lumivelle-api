using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class StylePreferenceDocument : DocumentDbEntity
{
    public ObjectId AccountId { get; set; }
    public string[] Styles { get; set; } = [];
    public string[] Goals { get; set; } = [];
}