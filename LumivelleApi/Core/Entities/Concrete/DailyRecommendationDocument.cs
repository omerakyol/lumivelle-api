using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class DailyEditItemValue
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}

public class MakeupRecItemValue
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Icon { get; set; }
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

public class ColorItemValue
{
    public string Name { get; set; }
    public string Hex { get; set; }
}

public class MakeupDetailItemValue
{
    public string Type { get; set; }
    public string Value { get; set; }
    public string Icon { get; set; }
}

public class AccessoryItemValue
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
    public ColorItemValue[] Colors { get; set; } = [];
    public MakeupDetailItemValue[] MakeupDetails { get; set; } = [];
    public AccessoryItemValue[] Accessories { get; set; } = [];
}