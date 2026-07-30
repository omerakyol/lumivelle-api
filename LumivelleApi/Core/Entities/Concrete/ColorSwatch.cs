namespace Core.Entities.Concrete;

public class ColorSwatch
{
    public string Name { get; set; } // generic color name, e.g. "Beige"
    public string Code { get; set; } // stylist/marketing code name, e.g. "Neutral Beige"
    public string Hex { get; set; } // e.g. "#E8DCC8"
}
