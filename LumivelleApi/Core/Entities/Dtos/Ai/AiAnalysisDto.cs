using Core.Entities.Concrete;

namespace Core.Entities.Dtos.Ai;

public class AiAnalysisDto
{
    public string Season { get; set; }
    public string Undertone { get; set; }
    public string Contrast { get; set; }
    public string FaceShape { get; set; }
    public string HairType { get; set; }
    public ColorSwatch[] Palette { get; set; }
    public ColorSwatch[] BestColors { get; set; }
    public ColorSwatch[] NeutralColors { get; set; }
    public ColorSwatch[] AvoidColors { get; set; }
    public MakeupBreakdown MakeupBreakdown { get; set; }
    public HairMetrics HairMetrics { get; set; }
    public string SkinType { get; set; }
    public string[] SkinConcerns { get; set; }
    public string SkinAnalysisNotes { get; set; }
    public ColorSwatch SkinTone { get; set; }
    public string MetalTone { get; set; }
    public string[] RecommendedProductCategories { get; set; }
    public string[] StyleReferences { get; set; }
    public string Headline { get; set; }
    public string Description { get; set; }
}