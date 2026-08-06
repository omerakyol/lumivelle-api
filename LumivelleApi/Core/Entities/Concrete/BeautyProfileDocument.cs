using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class HairMetrics
{
    public string FaceShapeDetail { get; set; }
    public string Jawline { get; set; }
    public string Forehead { get; set; }
    public string Density { get; set; }
}

public class TieredMakeupLook
{
    public string Id { get; set; }
    public string Title { get; set; }
    public ColorSwatch Lips { get; set; }
    public ColorSwatch Cheeks { get; set; }
    public ColorSwatch Contour { get; set; }
    public ColorSwatch Eyeshadow { get; set; }
    public ColorSwatch Liner { get; set; }
    public ColorSwatch Brow { get; set; }
}

public class TieredHairstyle
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

public class TieredStyleDna
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string[] SignaturePieces { get; set; } = [];
    public string[] Keywords { get; set; } = [];
}

public class BeautyProfileDocument : DocumentDbEntity
{
    public ObjectId AccountId { get; set; }
    public string Season { get; set; }
    public string Undertone { get; set; }
    public string Contrast { get; set; }
    public string FaceShape { get; set; }
    public string HairType { get; set; }
    public string EyeColor { get; set; }
    public string EyeShape { get; set; }
    public ColorSwatch[] Palette { get; set; } = [];
    public ColorSwatch[] BestColors { get; set; } = [];
    public ColorSwatch[] NeutralColors { get; set; } = [];
    public ColorSwatch[] AvoidColors { get; set; } = [];
    public TieredMakeupLook[] BestMakeupLooks { get; set; } = [];
    public TieredMakeupLook[] GoodMakeupLooks { get; set; } = [];
    public TieredMakeupLook[] AvoidMakeupLooks { get; set; } = [];
    public TieredHairstyle[] BestHairstyles { get; set; } = [];
    public TieredHairstyle[] GoodHairstyles { get; set; } = [];
    public TieredStyleDna[] BestStyleDnas { get; set; } = [];
    public HairMetrics HairMetrics { get; set; }
    public string SkinType { get; set; }
    public string[] SkinConcerns { get; set; } = [];
    public string SkinAnalysisNotes { get; set; }
    public ColorSwatch SkinTone { get; set; }
    public string MetalTone { get; set; }
    public string Headline { get; set; }
    public string Description { get; set; }
    public string RawAnalysisJson { get; set; }
    public string PhotoUrl { get; set; }
}
