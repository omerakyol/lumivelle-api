namespace Business.Handlers.Wardrobe.Commands.AnalyzeWardrobeItem;

public class AnalyzeWardrobeItemResult
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string[] Colors { get; set; } = [];
    public string[] StyleTags { get; set; } = [];
    public int PaletteMatchScore { get; set; }
}
