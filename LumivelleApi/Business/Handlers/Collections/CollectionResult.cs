using Entities.Concrete;

namespace Business.Handlers.Collections;

public class CollectionResult
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int PostCount { get; set; }
    public string[] PreviewImageUrls { get; set; } = [];
    public bool IsDefault { get; set; }

    public static CollectionResult FromDocument(
        CollectionDocument document, int postCount, string[] previewImageUrls)
    {
        return new CollectionResult
        {
            Id = document.Id.ToString(),
            Name = document.Name,
            PostCount = postCount,
            PreviewImageUrls = previewImageUrls,
            IsDefault = false
        };
    }
}
