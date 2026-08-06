using System.Collections.Generic;

namespace Core.Entities.Concrete;

public class StyleDnaDocument : DocumentDbEntity
{
    public Dictionary<string, string> Name { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
    public string[] CompatibleSeasons { get; set; } = [];
    public string[] CompatibleContrast { get; set; } = [];
    public string[] Palette { get; set; } = []; // hex strings
    public Dictionary<string, string[]> SignaturePieces { get; set; } = new();
    public Dictionary<string, string[]> Keywords { get; set; } = new();
    public int SortOrder { get; set; }
}
