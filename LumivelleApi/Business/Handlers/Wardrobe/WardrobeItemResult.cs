using System;
using Entities.Concrete;

namespace Business.Handlers.Wardrobe;

public class WardrobeItemResult
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string[] Colors { get; set; }
    public string[] StyleTags { get; set; }
    public int PaletteMatchScore { get; set; }
    public string ImageUrl { get; set; }
    public bool IsFavorite { get; set; }
    public int WearCount { get; set; }
    public DateTime? LastWornAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public static WardrobeItemResult FromDocument(WardrobeItemDocument document)
    {
        return new WardrobeItemResult
        {
            Id = document.Id.ToString(),
            Name = document.Name,
            Category = document.Category,
            Colors = document.Colors,
            StyleTags = document.StyleTags,
            PaletteMatchScore = document.PaletteMatchScore,
            ImageUrl = document.ImageUrl,
            IsFavorite = document.IsFavorite,
            WearCount = document.WearCount,
            LastWornAt = document.LastWornAt,
            CreatedAt = document.CreatedAt
        };
    }
}
