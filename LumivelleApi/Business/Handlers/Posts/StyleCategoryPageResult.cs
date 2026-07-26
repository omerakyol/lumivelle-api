namespace Business.Handlers.Posts;

public class StyleCategoryPageResult
{
    public string StyleTag { get; set; }
    public int TotalPostCount { get; set; }
    public string[] SecondaryTags { get; set; } = [];
    public FeedPageResult Page { get; set; }
}
