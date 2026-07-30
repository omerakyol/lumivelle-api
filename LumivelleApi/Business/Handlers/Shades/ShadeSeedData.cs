using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Business.Handlers.Shades;

public static class ShadeSeedData
{
    public static List<ShadeDocument> All()
    {
        var shades = new List<ShadeDocument>();
        var order = 0;

        foreach (var (name, hex) in Lips)
            shades.Add(new ShadeDocument { Category = "Lips", Name = name, Hex = hex, SortOrder = order++ });

        order = 0;
        foreach (var (name, hex) in Cheeks)
            shades.Add(new ShadeDocument { Category = "Cheeks", Name = name, Hex = hex, SortOrder = order++ });

        order = 0;
        foreach (var (name, hex) in Eyes)
            shades.Add(new ShadeDocument { Category = "Eyes", Name = name, Hex = hex, SortOrder = order++ });

        return shades;
    }

    private static readonly (string Name, string Hex)[] Lips =
    [
        ("Terracotta Nude", "#B5635A"),
        ("Rosewood Satin", "#9C5A50"),
        ("Spiced Clay", "#8C4A3E"),
        ("Coral Blush", "#C57B6B"),
        ("Berry Brick", "#A85248"),
        ("Soft Blush", "#CE9080")
    ];

    private static readonly (string Name, string Hex)[] Cheeks =
    [
        ("Peach Cream", "#D99A86"),
        ("Apricot Flush", "#E0A98F"),
        ("Soft Brick", "#C98072"),
        ("Warm Nude", "#E8B79E"),
        ("Clay Rose", "#B06B5C")
    ];

    private static readonly (string Name, string Hex)[] Eyes =
    [
        ("Champagne Shimmer", "#C9A46A"),
        ("Sand Glow", "#E2CA9B"),
        ("Warm Rosewood", "#9C7C72"),
        ("Cocoa Matte", "#6E4A3C"),
        ("Soft Taupe", "#B58E84")
    ];
}
