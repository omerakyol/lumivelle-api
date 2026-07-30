using MongoDB.Bson;

namespace Core.Entities.Concrete;

public class MakeupBreakdown
{
    public string Lips { get; set; }
    public string LipsHex { get; set; }
    public string Cheeks { get; set; }
    public string CheeksHex { get; set; }
    public string Contour { get; set; }
    public string ContourHex { get; set; }
    public string Eyeshadow { get; set; }
    public string EyeshadowHex { get; set; }
    public string Liner { get; set; }
    public string LinerHex { get; set; }
    public string Brow { get; set; }
    public string BrowHex { get; set; }
}

public class HairMetrics
{
    public string FaceShapeDetail { get; set; }
    public string Jawline { get; set; }
    public string Forehead { get; set; }
    public string Density { get; set; }
}

public class BeautyProfileDocument : DocumentDbEntity
{
    public ObjectId AccountId { get; set; }
    public string Season { get; set; }
    public string Undertone { get; set; }
    public string Contrast { get; set; }
    public string FaceShape { get; set; }
    public string HairType { get; set; }
    public ColorSwatch[] Palette { get; set; } = [];
    public ColorSwatch[] BestColors { get; set; } = [];
    public ColorSwatch[] NeutralColors { get; set; } = [];
    public ColorSwatch[] AvoidColors { get; set; } = [];
    public MakeupBreakdown MakeupBreakdown { get; set; }
    public HairMetrics HairMetrics { get; set; }
    public string SkinType { get; set; }
    public string[] SkinConcerns { get; set; } = [];
    public string SkinAnalysisNotes { get; set; }
    public ColorSwatch SkinTone { get; set; } // detected natural skin tone, for foundation/product shade matching
    public string MetalTone { get; set; } // "Gold", "Silver", "Rose Gold" or "Neutral" — for jewelry/accessory recs
    public string[] RecommendedProductCategories { get; set; } = []; // e.g. "Foundation", "Blush", "Lipstick"
    public string[] StyleReferences { get; set; } = [];
    public string Headline { get; set; }
    public string Description { get; set; }
    public string RawAnalysisJson { get; set; }
    public string PhotoUrl { get; set; } // served from MongoDB GridFS, not disk — see IMediaFileRepository
}