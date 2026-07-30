using Core.Entities;

namespace Core.Entities.Concrete;

public class ShadeDocument : DocumentDbEntity
{
    public string Category { get; set; }
    public string Name { get; set; }
    public string Hex { get; set; }
    public int SortOrder { get; set; }
}
