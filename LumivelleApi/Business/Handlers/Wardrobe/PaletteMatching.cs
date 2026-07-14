using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Handlers.Wardrobe;

public static class PaletteMatching
{
    public static int ScoreColorAgainstPalette(string hexColor, string[] palette)
    {
        if (string.IsNullOrWhiteSpace(hexColor) || palette == null || palette.Length == 0)
            return 0;

        var best = palette
            .Select(p => ScoreColorPair(hexColor, p))
            .DefaultIfEmpty(0)
            .Max();

        return best;
    }

    public static int ScoreColorsAgainstPalette(string[] colors, string[] palette)
    {
        if (colors == null || colors.Length == 0)
            return 0;

        return colors
            .Select(c => ScoreColorAgainstPalette(c, palette))
            .DefaultIfEmpty(0)
            .Max();
    }

    public static bool IsColorCoveredByWardrobe(
        string paletteColor, IEnumerable<string> wardrobeColors, int coverageThreshold = 60)
    {
        return wardrobeColors.Any(c => ScoreColorPair(c, paletteColor) >= coverageThreshold);
    }

    private static int ScoreColorPair(string hexA, string hexB)
    {
        if (!TryParseHex(hexA, out var a) || !TryParseHex(hexB, out var b))
            return 0;

        var distance = Math.Sqrt(
            Math.Pow(a.r - b.r, 2) + Math.Pow(a.g - b.g, 2) + Math.Pow(a.b - b.b, 2));

        // Max possible RGB distance is sqrt(255^2 * 3) ≈ 441.7
        const double maxDistance = 441.7;
        var normalized = 1 - distance / maxDistance;
        return (int)Math.Round(Math.Clamp(normalized, 0, 1) * 100);
    }

    private static bool TryParseHex(string hex, out (int r, int g, int b) rgb)
    {
        rgb = (0, 0, 0);
        if (string.IsNullOrWhiteSpace(hex))
            return false;

        var value = hex.TrimStart('#');
        if (value.Length != 6)
            return false;

        try
        {
            rgb = (
                Convert.ToInt32(value.Substring(0, 2), 16),
                Convert.ToInt32(value.Substring(2, 2), 16),
                Convert.ToInt32(value.Substring(4, 2), 16));
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
