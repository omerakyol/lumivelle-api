using System.Collections.Generic;

namespace Business.Handlers.Wardrobe.Queries.GetOutfitSuggestions;

public class OutfitComboItemResult
{
    public string Category { get; set; }
    public string WardrobeItemId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}

public class OutfitComboResult
{
    public List<OutfitComboItemResult> Items { get; set; } = [];
    public int Score { get; set; }
    public string Why { get; set; }
}

public class OutfitSuggestionsResult
{
    public List<OutfitComboResult> Combos { get; set; } = [];
}
