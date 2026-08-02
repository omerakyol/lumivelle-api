using System.Collections.Generic;

namespace Core.Entities.Concrete;

public class MakeupRecPresetValue
{
    // Keyed by 2-letter language code ("en", "tr", "fr", "es", "ar", "ru")
    public Dictionary<string, string> Title { get; set; } = new();
    public Dictionary<string, string> Subtitle { get; set; } = new();
    public string Icon { get; set; }
}

public class DailyEditPresetDocument : DocumentDbEntity
{
    public string SeasonFamily { get; set; } // "Spring" | "Summer" | "Autumn" | "Winter"
    public string Undertone { get; set; } // "Warm" | "Cool" | "Neutral"
    public string Contrast { get; set; } // "Low" | "Medium" | "High"

    // Keyed by 2-letter language code ("en", "tr", "fr", "es", "ar", "ru")
    public Dictionary<string, string> DailyEditTitle { get; set; } = new();
    public Dictionary<string, string> DailyEditSubtitle { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
    public MakeupRecPresetValue[] MakeupRecs { get; set; } = [];
    public List<Dictionary<string, string>> AccessoryTitles { get; set; } = [];
    public int SortOrder { get; set; }
}
