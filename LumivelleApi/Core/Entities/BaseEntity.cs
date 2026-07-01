using System;
using Core.Enums;

namespace Core.Entities;

public abstract class BaseEntity : IEntity
{
    public virtual Guid Id { get; set; } = Guid.CreateVersion7();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;
}