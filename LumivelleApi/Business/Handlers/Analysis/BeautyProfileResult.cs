using System;
using Entities.Concrete;

namespace Business.Handlers.Analysis;

public class BeautyProfileResult
{
    public string Id { get; set; }
    public string Season { get; set; }
    public string Undertone { get; set; }
    public string Contrast { get; set; }
    public string FaceShape { get; set; }
    public string HairType { get; set; }
    public string[] Palette { get; set; }
    public string[] BestColors { get; set; }
    public string[] NeutralColors { get; set; }
    public string[] AvoidColors { get; set; }
    public MakeupBreakdown MakeupBreakdown { get; set; }
    public HairMetrics HairMetrics { get; set; }
    public string[] StyleReferences { get; set; }
    public string Headline { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

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
            Palette = document.Palette,
            BestColors = document.BestColors,
            NeutralColors = document.NeutralColors,
            AvoidColors = document.AvoidColors,
            MakeupBreakdown = document.MakeupBreakdown,
            HairMetrics = document.HairMetrics,
            StyleReferences = document.StyleReferences,
            Headline = document.Headline,
            Description = document.Description,
            CreatedAt = document.CreatedAt
        };
    }
}
