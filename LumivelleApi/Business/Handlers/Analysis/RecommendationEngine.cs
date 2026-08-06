using System.Linq;
using Core.Entities.Concrete;

namespace Business.Handlers.Analysis;

public enum RecommendationTier { Best, Good, Avoid }

public static class RecommendationEngine
{
    public static RecommendationTier ScoreMakeupLook(MakeupLookDocument look, BeautyProfileDocument profile)
    {
        var dims = 0;
        var matched = 0;
        if (look.CompatibleSeasons.Length > 0)
        {
            dims++;
            if (look.CompatibleSeasons.Contains(profile.Season)) matched++;
        }
        if (look.CompatibleUndertones.Length > 0)
        {
            dims++;
            if (look.CompatibleUndertones.Contains(profile.Undertone)) matched++;
        }
        if (look.CompatibleEyeColors.Length > 0)
        {
            dims++;
            if (look.CompatibleEyeColors.Contains(profile.EyeColor)) matched++;
        }
        return Bucket(matched, dims);
    }

    public static RecommendationTier ScoreHairstyle(HairstyleDocument hairstyle, BeautyProfileDocument profile)
    {
        var dims = 0;
        var matched = 0;
        if (hairstyle.CompatibleFaceShapes.Length > 0)
        {
            dims++;
            if (hairstyle.CompatibleFaceShapes.Contains(profile.FaceShape)) matched++;
        }
        if (hairstyle.CompatibleJawlines.Length > 0)
        {
            dims++;
            if (hairstyle.CompatibleJawlines.Contains(profile.HairMetrics?.Jawline)) matched++;
        }
        if (hairstyle.CompatibleDensities.Length > 0)
        {
            dims++;
            if (hairstyle.CompatibleDensities.Contains(profile.HairMetrics?.Density)) matched++;
        }
        return Bucket(matched, dims);
    }

    public static RecommendationTier ScoreStyleDna(StyleDnaDocument styleDna, BeautyProfileDocument profile)
    {
        var dims = 0;
        var matched = 0;
        if (styleDna.CompatibleSeasons.Length > 0)
        {
            dims++;
            if (styleDna.CompatibleSeasons.Contains(profile.Season)) matched++;
        }
        if (styleDna.CompatibleContrast.Length > 0)
        {
            dims++;
            if (styleDna.CompatibleContrast.Contains(profile.Contrast)) matched++;
        }
        return Bucket(matched, dims);
    }

    private static RecommendationTier Bucket(int matched, int dims)
    {
        if (dims == 0) return RecommendationTier.Good;
        var ratio = (double)matched / dims;
        if (ratio >= 0.67) return RecommendationTier.Best;
        return matched > 0 ? RecommendationTier.Good : RecommendationTier.Avoid;
    }
}
