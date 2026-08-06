using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Business.Handlers.Analysis;

public static class ColorPaletteSeedData
{
    public static List<ColorPaletteDocument> All() => new()
    {
        Palette("Spring",
            best: new[] { Swatch("Coral", "Warm Coral", "#F2846B"), Swatch("Peach", "Soft Peach", "#F4B183"), Swatch("Turquoise", "Clear Turquoise", "#5FBFB3"), Swatch("Golden Yellow", "Sunlit Gold", "#F2C464"), Swatch("Warm Green", "Spring Green", "#8FBF5A") },
            neutral: new[] { Swatch("Ivory", "Warm Ivory", "#F5EBD8"), Swatch("Camel", "Light Camel", "#D9B48C"), Swatch("Warm Gray", "Soft Warm Gray", "#B8AFA3") },
            avoid: new[] { Swatch("Icy Blue", "Icy Cool Blue", "#B8D4E3"), Swatch("Black", "Stark Black", "#0E0E14") }),
        Palette("Light Spring",
            best: new[] { Swatch("Light Coral", "Pale Coral", "#F5A18C"), Swatch("Light Aqua", "Pale Aqua", "#8FD4C9"), Swatch("Butter Yellow", "Soft Butter", "#F5D98A"), Swatch("Light Peach", "Whisper Peach", "#F7C9A8"), Swatch("Mint", "Fresh Mint", "#A3D9B8") },
            neutral: new[] { Swatch("Cream", "Soft Cream", "#F7EFDD"), Swatch("Light Camel", "Pale Camel", "#E0C39F") },
            avoid: new[] { Swatch("Charcoal", "Deep Charcoal", "#3A3A3F"), Swatch("Burgundy", "Deep Burgundy", "#5C1F2E") }),
        Palette("Warm Spring",
            best: new[] { Swatch("Tomato", "Warm Tomato", "#E8583B"), Swatch("Marigold", "Bright Marigold", "#F0A93B"), Swatch("Grass Green", "Warm Grass", "#7CAB3E"), Swatch("Turquoise", "Warm Turquoise", "#3FB6A8"), Swatch("Warm Pink", "Coral Pink", "#F0837E") },
            neutral: new[] { Swatch("Golden Beige", "Warm Golden Beige", "#D9BC8C"), Swatch("Warm Taupe", "Golden Taupe", "#B39B7C") },
            avoid: new[] { Swatch("Cool Pink", "Icy Pink", "#EFD5E5"), Swatch("Silver Gray", "Cool Silver", "#B8BCC4") }),
        Palette("Clear Spring",
            best: new[] { Swatch("True Red", "Clear Red", "#E0342B"), Swatch("Emerald", "Clear Emerald", "#28A870"), Swatch("Royal Blue", "Clear Royal Blue", "#2E5FD9"), Swatch("Hot Pink", "Vivid Pink", "#EF488F"), Swatch("Lemon Yellow", "Clear Lemon", "#F5DE3F") },
            neutral: new[] { Swatch("Pure White", "Bright White", "#FBFBF8"), Swatch("Warm Navy", "Clear Navy", "#1E2E5C") },
            avoid: new[] { Swatch("Dusty Rose", "Muted Dusty Rose", "#C79A96"), Swatch("Beige", "Muted Beige", "#CBBBA3") }),
        Palette("Summer",
            best: new[] { Swatch("Powder Blue", "Soft Powder Blue", "#A8C4D9"), Swatch("Lavender", "Cool Lavender", "#B8A8D0"), Swatch("Rose Pink", "Cool Rose", "#D89AAE"), Swatch("Sage", "Cool Sage", "#9BAE9B"), Swatch("Slate Gray", "Cool Slate", "#8B98A3") },
            neutral: new[] { Swatch("Cool Beige", "Soft Cool Beige", "#D4C7B8"), Swatch("Dove Gray", "Soft Dove Gray", "#B8B4AE") },
            avoid: new[] { Swatch("Orange", "Bright Orange", "#F0762E"), Swatch("Golden Yellow", "Warm Gold", "#F2C464") }),
        Palette("Light Summer",
            best: new[] { Swatch("Baby Blue", "Pale Baby Blue", "#BFD8E8"), Swatch("Soft Lilac", "Pale Lilac", "#CBBEDD"), Swatch("Powder Pink", "Pale Powder Pink", "#E5C2CE"), Swatch("Pale Aqua", "Whisper Aqua", "#B8DAD4"), Swatch("Light Gray", "Soft Light Gray", "#C7C7C9") },
            neutral: new[] { Swatch("Soft Ivory", "Cool Soft Ivory", "#EFE9DE"), Swatch("Light Taupe", "Cool Light Taupe", "#C9BFB2") },
            avoid: new[] { Swatch("Black", "Stark Black", "#0E0E14"), Swatch("Rust", "Deep Rust", "#A8502E") }),
        Palette("Cool Summer",
            best: new[] { Swatch("Cool Rose", "Deep Cool Rose", "#C77E90"), Swatch("Periwinkle", "Cool Periwinkle", "#8C9AD9"), Swatch("Sea Green", "Cool Sea Green", "#5FA394"), Swatch("Plum", "Soft Plum", "#8C5A79"), Swatch("Cool Gray", "Blue Gray", "#9AA3AE") },
            neutral: new[] { Swatch("Taupe", "Cool Taupe", "#B0A198"), Swatch("Charcoal", "Soft Charcoal", "#5C5C63") },
            avoid: new[] { Swatch("Orange", "Warm Orange", "#F0762E"), Swatch("Olive", "Warm Olive", "#7A7A32") }),
        Palette("Soft Summer",
            best: new[] { Swatch("Dusty Rose", "Soft Dusty Rose", "#C79A96"), Swatch("Muted Teal", "Soft Muted Teal", "#6E9B96"), Swatch("Mauve", "Soft Mauve", "#A88CA0"), Swatch("Soft Blue", "Muted Soft Blue", "#8FAEC2"), Swatch("Sage Green", "Muted Sage", "#93A889") },
            neutral: new[] { Swatch("Greige", "Soft Greige", "#C2B8AC"), Swatch("Soft Gray", "Muted Soft Gray", "#ADA9A3") },
            avoid: new[] { Swatch("Neon Pink", "Vivid Neon Pink", "#F23FA0"), Swatch("Bright Orange", "Vivid Orange", "#F0762E") }),
        Palette("Autumn",
            best: new[] { Swatch("Rust", "Warm Rust", "#B5522E"), Swatch("Olive", "Warm Olive", "#7A7A32"), Swatch("Mustard", "Warm Mustard", "#C99A2E"), Swatch("Terracotta", "Warm Terracotta", "#B8665C"), Swatch("Forest Green", "Deep Forest", "#3E5C3A") },
            neutral: new[] { Swatch("Camel", "Warm Camel", "#B08A5A"), Swatch("Espresso", "Deep Espresso", "#5B4434") },
            avoid: new[] { Swatch("Icy Pink", "Cool Icy Pink", "#EFD5E5"), Swatch("Cool Gray", "Cool Silver Gray", "#B8BCC4") }),
        Palette("Soft Autumn",
            best: new[] { Swatch("Champagne", "Soft Champagne", "#C9A46A"), Swatch("Terracotta", "Soft Terracotta", "#B8665C"), Swatch("Olive", "Soft Olive", "#7A7A32"), Swatch("Bronze", "Soft Bronze", "#8A6A2B"), Swatch("Muted Teal", "Soft Muted Teal", "#6E8A82") },
            neutral: new[] { Swatch("Warm Sand", "Soft Warm Sand", "#D8C08A"), Swatch("Taupe", "Soft Warm Taupe", "#9B8572"), Swatch("Espresso", "Soft Espresso", "#5B4434") },
            avoid: new[] { Swatch("Icy Pink", "Cool Icy Pink", "#EFD5E5"), Swatch("Cool Gray", "Cool Silver Gray", "#B8BCC4") }),
        Palette("Warm Autumn",
            best: new[] { Swatch("Pumpkin", "Deep Pumpkin", "#D97C34"), Swatch("Golden Olive", "Rich Golden Olive", "#8A8232"), Swatch("Brick Red", "Warm Brick", "#A8462E"), Swatch("Mustard", "Deep Mustard", "#C99A2E"), Swatch("Chestnut", "Warm Chestnut", "#7A4B34") },
            neutral: new[] { Swatch("Golden Camel", "Deep Golden Camel", "#B08040"), Swatch("Chocolate", "Warm Chocolate", "#4A3324") },
            avoid: new[] { Swatch("Cool Pink", "Icy Cool Pink", "#EFD5E5"), Swatch("Lavender", "Cool Lavender", "#B8A8D0") }),
        Palette("Deep Autumn",
            best: new[] { Swatch("Deep Rust", "Rich Deep Rust", "#9C4126"), Swatch("Forest Green", "Deep Forest Green", "#2E4A2A"), Swatch("Aubergine", "Deep Aubergine", "#4A2E3A"), Swatch("Bronze", "Deep Bronze", "#7A5A20"), Swatch("Deep Teal", "Rich Deep Teal", "#264A48") },
            neutral: new[] { Swatch("Espresso", "Deep Espresso", "#4A342A"), Swatch("Deep Camel", "Rich Deep Camel", "#8A6440") },
            avoid: new[] { Swatch("Pastel Pink", "Pale Pastel Pink", "#F5DDE5"), Swatch("Ice Blue", "Pale Ice Blue", "#D5E8F0") }),
        Palette("Winter",
            best: new[] { Swatch("True Red", "Cool True Red", "#C81E2E"), Swatch("Royal Blue", "Cool Royal Blue", "#1E3E9C"), Swatch("Emerald", "Cool Emerald", "#0E7A54"), Swatch("Fuchsia", "Vivid Fuchsia", "#C82E8A"), Swatch("Black", "True Black", "#0E0E14") },
            neutral: new[] { Swatch("Pure White", "Bright Pure White", "#FBFBF8"), Swatch("Charcoal", "Cool Charcoal", "#3A3A3F") },
            avoid: new[] { Swatch("Orange", "Warm Orange", "#F0762E"), Swatch("Mustard", "Warm Mustard", "#C99A2E") }),
        Palette("Deep Winter",
            best: new[] { Swatch("Wine", "Deep Wine", "#6E1E30"), Swatch("Navy", "Deep Navy", "#101E42"), Swatch("Deep Emerald", "Rich Deep Emerald", "#0A5C3E"), Swatch("Black", "True Black", "#0E0E14"), Swatch("Deep Fuchsia", "Rich Fuchsia", "#8A1E5C") },
            neutral: new[] { Swatch("Charcoal", "Deep Charcoal", "#2A2A30"), Swatch("Deep Taupe", "Cool Deep Taupe", "#4A423C") },
            avoid: new[] { Swatch("Pastel Yellow", "Pale Pastel Yellow", "#F5EBA8"), Swatch("Peach", "Soft Peach", "#F4B183") }),
        Palette("Cool Winter",
            best: new[] { Swatch("Icy Blue", "Cool Icy Blue", "#7EA8C4"), Swatch("Magenta", "Cool Magenta", "#C81E8A"), Swatch("Cool Red", "Blue-Based Red", "#C81E2E"), Swatch("Silver Gray", "Cool Silver Gray", "#9AA3AE"), Swatch("Cool Emerald", "Cool Emerald", "#0E7A6A") },
            neutral: new[] { Swatch("Cool White", "Icy Cool White", "#F0F2F5"), Swatch("Charcoal", "Cool Charcoal", "#3A3A3F") },
            avoid: new[] { Swatch("Warm Orange", "Deep Warm Orange", "#D9642E"), Swatch("Golden Yellow", "Warm Gold", "#F2C464") }),
        Palette("Clear Winter",
            best: new[] { Swatch("Vivid Red", "Clear Vivid Red", "#E0142E"), Swatch("Royal Purple", "Clear Royal Purple", "#4A1E9C"), Swatch("Clear Turquoise", "Vivid Turquoise", "#0EA8A0"), Swatch("Hot Pink", "Clear Hot Pink", "#EF4B8F"), Swatch("Black", "True Black", "#0E0E14") },
            neutral: new[] { Swatch("Pure White", "Bright Pure White", "#FBFBF8"), Swatch("Cool Navy", "Clear Cool Navy", "#101E42") },
            avoid: new[] { Swatch("Muted Beige", "Dusty Muted Beige", "#CBBBA3"), Swatch("Dusty Rose", "Soft Dusty Rose", "#C79A96") }),
    };

    private static ColorPaletteDocument Palette(string season, ColorSwatch[] best, ColorSwatch[] neutral, ColorSwatch[] avoid) => new()
    {
        Season = season,
        BestColors = best,
        NeutralColors = neutral,
        AvoidColors = avoid
    };

    private static ColorSwatch Swatch(string name, string code, string hex) => new() { Name = name, Code = code, Hex = hex };
}
