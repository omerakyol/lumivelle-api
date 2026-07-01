using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.Concrete;

public class RefreshToken : DocumentDbEntity
{
    public ObjectId AccountId { get; set; }
    public string Token { get; set; }
    public bool IsRevoked { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime ExpiredAt { get; set; }
}