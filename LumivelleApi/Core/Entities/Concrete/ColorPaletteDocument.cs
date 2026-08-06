namespace Core.Entities.Concrete;

public class ColorPaletteDocument : DocumentDbEntity
{
    public string Season { get; set; } // one of the 16 existing Season values, unique per document
    public ColorSwatch[] BestColors { get; set; } = [];
    public ColorSwatch[] NeutralColors { get; set; } = [];
    public ColorSwatch[] AvoidColors { get; set; } = [];
}
