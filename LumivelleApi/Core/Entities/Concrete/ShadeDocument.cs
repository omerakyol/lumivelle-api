using System.Collections.Generic;
using Core.Entities;

namespace Core.Entities.Concrete;

public class ShadeDocument : DocumentDbEntity
{
    public string Category { get; set; } // internal grouping code, not translated: "Lips" | "Cheeks" | "Eyes"

    // Keyed by 2-letter language code ("en", "tr", "fr", "es", "ar", "ru")
    public Dictionary<string, string> Name { get; set; } = new();
    public string Hex { get; set; }
    public int SortOrder { get; set; }
}
