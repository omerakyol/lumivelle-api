using System.Collections.Generic;

namespace Business.Handlers.Recommendations.Queries.GetDailyEdit;

public class DailyEditItem
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string ImageUrl { get; set; }
}

public class MakeupRecItem
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
}

public class OutfitRecItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int MatchScore { get; set; }
    public string ImageUrl { get; set; }
}

public class TrendingItem
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
}

public class DailyEditResult
{
    public string Season { get; set; }
    public string[] Palette { get; set; } = [];
    public DailyEditItem DailyEdit { get; set; }
    public List<MakeupRecItem> MakeupRecs { get; set; } = [];
    public OutfitRecItem OutfitRec { get; set; }
    public List<TrendingItem> Trending { get; set; } = [];
}
