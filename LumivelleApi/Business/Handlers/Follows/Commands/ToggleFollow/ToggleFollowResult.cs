namespace Business.Handlers.Follows.Commands.ToggleFollow;

public class ToggleFollowResult
{
    public bool IsFollowedByMe { get; set; }
    public int FollowerCount { get; set; }
}
