using Core.Entities;
using MongoDB.Bson;

namespace Entities.Concrete;

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
    public string[] Palette { get; set; } = [];
    public string[] BestColors { get; set; } = [];
    public string[] NeutralColors { get; set; } = [];
    public string[] AvoidColors { get; set; } = [];
    public MakeupBreakdown MakeupBreakdown { get; set; }
    public HairMetrics HairMetrics { get; set; }
    public string[] StyleReferences { get; set; } = [];
    public string Headline { get; set; }
    public string Description { get; set; }
    public string RawAnalysisJson { get; set; }
}
