using System.Collections.Generic;

namespace Core.Entities.Concrete;

public class MakeupLookDocument : DocumentDbEntity
{
    public Dictionary<string, string> Title { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
    public string Category { get; set; } // "Natural" | "Everyday" | "SoftGlam" | "FullGlam" | "Smokey" | "Bridal" | "Seasonal"
    public string[] CompatibleSeasons { get; set; } = [];
    public string[] CompatibleUndertones { get; set; } = [];
    public string[] CompatibleEyeColors { get; set; } = [];
    public ColorSwatch Lips { get; set; }
    public ColorSwatch Cheeks { get; set; }
    public ColorSwatch Contour { get; set; }
    public ColorSwatch Eyeshadow { get; set; }
    public ColorSwatch Liner { get; set; }
    public ColorSwatch Brow { get; set; }
    public int SortOrder { get; set; }
}
