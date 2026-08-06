using System.Collections.Generic;

namespace Core.Entities.Concrete;

public class HairstyleDocument : DocumentDbEntity
{
    public Dictionary<string, string> Title { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
    public string Category { get; set; } // "BaseCut" | "Bangs" | "Texture"
    public string[] CompatibleFaceShapes { get; set; } = [];
    public string[] CompatibleJawlines { get; set; } = [];
    public string[] CompatibleDensities { get; set; } = [];
    public int SortOrder { get; set; }
}
