using System;
using Core.Entities;
using MongoDB.Bson;

namespace Entities.Concrete;

public class WardrobeItemDocument : DocumentDbEntity
{
    public ObjectId AccountId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string[] Colors { get; set; } = [];
    public string[] StyleTags { get; set; } = [];
    public int PaletteMatchScore { get; set; }
    public string ImageUrl { get; set; }
    public bool IsFavorite { get; set; }
    public int WearCount { get; set; }
    public DateTime? LastWornAt { get; set; }
}
