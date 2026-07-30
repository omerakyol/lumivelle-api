using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class DailyEditItemValue
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string ImageUrl { get; set; }
}

public class MakeupRecItemValue
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
}

public class OutfitRecItemValue
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int MatchScore { get; set; }
    public string ImageUrl { get; set; }
    public ObjectId? WardrobeItemId { get; set; }
}

public class TrendingItemValue
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
}

public class DailyRecommendationDocument : DocumentDbEntity
{
    public ObjectId AccountId { get; set; }
    public string LocalDate { get; set; }
    public string Season { get; set; }
    public string[] Palette { get; set; } = [];
    public DailyEditItemValue DailyEdit { get; set; }
    public MakeupRecItemValue[] MakeupRecs { get; set; } = [];
    public OutfitRecItemValue OutfitRec { get; set; }
    public TrendingItemValue[] Trending { get; set; } = [];
}