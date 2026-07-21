using System.Collections.Generic;

namespace Business.Handlers.Comments;

public class CommentPageResult
{
    public List<CommentResult> Comments { get; set; } = [];
    public string NextCursor { get; set; }
}
