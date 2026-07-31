namespace Core.Entities.Concrete;

public class MakeupRecPresetValue
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Icon { get; set; }
}

public class DailyEditPresetDocument : DocumentDbEntity
{
    public string SeasonFamily { get; set; } // "Spring" | "Summer" | "Autumn" | "Winter"
    public string Undertone { get; set; } // "Warm" | "Cool" | "Neutral"
    public string Contrast { get; set; } // "Low" | "Medium" | "High"
    public string DailyEditTitle { get; set; }
    public string DailyEditSubtitle { get; set; }
    public string Description { get; set; }
    public MakeupRecPresetValue[] MakeupRecs { get; set; } = [];
    public string[] AccessoryTitles { get; set; } = [];
    public int SortOrder { get; set; }
}
