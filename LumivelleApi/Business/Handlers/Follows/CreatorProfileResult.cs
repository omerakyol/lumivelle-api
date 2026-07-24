namespace Business.Handlers.Follows;

public class CreatorProfileResult
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }
    public string Bio { get; set; }
    public bool IsVerified { get; set; }
    public bool IsCreator { get; set; }
    public string StyleTag { get; set; }
    public string Season { get; set; }
    public int PostCount { get; set; }
    public int FollowerCount { get; set; }
    public bool IsFollowedByMe { get; set; }
    public bool IsOwnProfile { get; set; }
}
