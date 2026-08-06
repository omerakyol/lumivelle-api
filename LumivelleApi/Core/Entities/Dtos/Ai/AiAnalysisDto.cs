using Core.Entities.Concrete;

namespace Core.Entities.Dtos.Ai;

public class AiAnalysisDto
{
    public string Season { get; set; }
    public string Undertone { get; set; }
    public string Contrast { get; set; }
    public string FaceShape { get; set; }
    public string HairType { get; set; }
    public string EyeColor { get; set; }
    public string EyeShape { get; set; }
    public HairMetrics HairMetrics { get; set; }
    public string SkinType { get; set; }
    public string[] SkinConcerns { get; set; }
    public string SkinAnalysisNotes { get; set; }
    public ColorSwatch SkinTone { get; set; }
    public string MetalTone { get; set; }
}
