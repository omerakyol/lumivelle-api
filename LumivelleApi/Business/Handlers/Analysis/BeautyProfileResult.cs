using System;
using Core.Entities.Concrete;

namespace Business.Handlers.Analysis;

public class BeautyProfileResult
{
    public string Id { get; set; }
    public string Season { get; set; }
    public string Undertone { get; set; }
    public string Contrast { get; set; }
    public string FaceShape { get; set; }
    public string HairType { get; set; }
    public string EyeColor { get; set; }
    public string EyeShape { get; set; }
    public ColorSwatch[] Palette { get; set; }
    public ColorSwatch[] BestColors { get; set; }
    public ColorSwatch[] NeutralColors { get; set; }
    public ColorSwatch[] AvoidColors { get; set; }
    public TieredMakeupLook[] BestMakeupLooks { get; set; }
    public TieredMakeupLook[] GoodMakeupLooks { get; set; }
    public TieredMakeupLook[] AvoidMakeupLooks { get; set; }
    public TieredHairstyle[] BestHairstyles { get; set; }
    public TieredHairstyle[] GoodHairstyles { get; set; }
    public TieredStyleDna[] BestStyleDnas { get; set; }
    public HairMetrics HairMetrics { get; set; }
    public string SkinType { get; set; }
    public string[] SkinConcerns { get; set; }
    public string SkinAnalysisNotes { get; set; }
    public ColorSwatch SkinTone { get; set; }
    public string MetalTone { get; set; }
    public string Headline { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PhotoUrl { get; set; }

    public static BeautyProfileResult FromDocument(BeautyProfileDocument document)
    {
        return new BeautyProfileResult
        {
            Id = document.Id.ToString(),
            Season = document.Season,
            Undertone = document.Undertone,
            Contrast = document.Contrast,
            FaceShape = document.FaceShape,
            HairType = document.HairType,
            EyeColor = document.EyeColor,
            EyeShape = document.EyeShape,
            Palette = document.Palette,
            BestColors = document.BestColors,
            NeutralColors = document.NeutralColors,
            AvoidColors = document.AvoidColors,
            BestMakeupLooks = document.BestMakeupLooks,
            GoodMakeupLooks = document.GoodMakeupLooks,
            AvoidMakeupLooks = document.AvoidMakeupLooks,
            BestHairstyles = document.BestHairstyles,
            GoodHairstyles = document.GoodHairstyles,
            BestStyleDnas = document.BestStyleDnas,
            HairMetrics = document.HairMetrics,
            SkinType = document.SkinType,
            SkinConcerns = document.SkinConcerns,
            SkinAnalysisNotes = document.SkinAnalysisNotes,
            SkinTone = document.SkinTone,
            MetalTone = document.MetalTone,
            Headline = document.Headline,
            Description = document.Description,
            CreatedAt = document.CreatedAt,
            PhotoUrl = document.PhotoUrl
        };
    }
}
