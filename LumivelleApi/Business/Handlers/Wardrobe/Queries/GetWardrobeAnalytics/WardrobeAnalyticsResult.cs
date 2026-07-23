using System.Collections.Generic;

namespace Business.Handlers.Wardrobe.Queries.GetWardrobeAnalytics;

public class ColorUsageResult
{
    public string Hex { get; set; }
    public string Label { get; set; }
    public int Percentage { get; set; }
}

public class StyleUsageResult
{
    public string Tag { get; set; }
    public int Percentage { get; set; }
}

public class SeasonUsageResult
{
    public string Season { get; set; }
    public int Count { get; set; }
}

public class WardrobeAnalyticsResult
{
    public int PaletteAlignmentScore { get; set; }
    public List<ColorUsageResult> MostWornColors { get; set; } = [];
    public List<StyleUsageResult> StyleDistribution { get; set; } = [];
    public List<SeasonUsageResult> SeasonalUsage { get; set; } = [];
}
