using System;
using Business.Handlers.Posts;
using Entities.Concrete;

namespace Business.Handlers.Comments;

public class CommentResult
{
    public string Id { get; set; }
    public string PostId { get; set; }
    public AuthorResult Author { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }

    public static CommentResult FromDocument(CommentDocument document, AuthorResult author)
    {
        return new CommentResult
        {
            Id = document.Id.ToString(),
            PostId = document.PostId.ToString(),
            Author = author,
            Text = document.Text,
            CreatedAt = document.CreatedAt
        };
    }
}
