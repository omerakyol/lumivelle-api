namespace Business.Handlers.Posts.Commands.ToggleLike;

public class ToggleLikeResult
{
    public bool IsLikedByMe { get; set; }
    public int LikeCount { get; set; }
}
