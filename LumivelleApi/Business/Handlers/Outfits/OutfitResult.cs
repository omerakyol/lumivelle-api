using System;
using System.Collections.Generic;
using Business.Handlers.Wardrobe;

namespace Business.Handlers.Outfits;

public class OutfitResult
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<WardrobeItemResult> Items { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}
