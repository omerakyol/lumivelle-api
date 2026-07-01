using System;
using Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;

public abstract class DocumentDbEntity : IEntity
{
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ObjectId CreatedBy { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? UpdatedAt { get; set; }

    public ObjectId? UpdatedBy { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? DeletedAt { get; set; }

    public ObjectId? DeletedBy { get; set; }

    public EntityStatus Status { get; set; } = EntityStatus.Active;
}