using System.Collections.Generic;

namespace Business.Handlers.Posts;

public class FeedPageResult
{
    public List<PostResult> Posts { get; set; } = [];
    public string NextCursor { get; set; }
}
