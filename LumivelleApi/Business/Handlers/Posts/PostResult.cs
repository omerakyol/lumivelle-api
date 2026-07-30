using System;
using Core.Entities.Concrete;

namespace Business.Handlers.Posts;

public class PostResult
{
    public string Id { get; set; }
    public string AccountId { get; set; }
    public string[] ImageUrls { get; set; }
    public string Caption { get; set; }
    public string WardrobeItemId { get; set; }
    public string OutfitId { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public bool IsLikedByMe { get; set; }
    public bool IsSavedByMe { get; set; }
    public AuthorResult Author { get; set; }
    public DateTime CreatedAt { get; set; }

    public static PostResult FromDocument(
        PostDocument document, AuthorResult author, bool isLikedByMe, bool isSavedByMe)
    {
        return new PostResult
        {
            Id = document.Id.ToString(),
            AccountId = document.AccountId.ToString(),
            ImageUrls = document.ImageUrls,
            Caption = document.Caption,
            WardrobeItemId = document.WardrobeItemId?.ToString(),
            OutfitId = document.OutfitId?.ToString(),
            LikeCount = document.LikeCount,
            CommentCount = document.CommentCount,
            IsLikedByMe = isLikedByMe,
            IsSavedByMe = isSavedByMe,
            Author = author,
            CreatedAt = document.CreatedAt
        };
    }
}