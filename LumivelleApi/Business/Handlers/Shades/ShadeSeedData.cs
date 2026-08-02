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

    private static Dictionary<string, string> Name(
        string en, string tr, string fr, string es, string ar, string ru) => new()
    {
        ["en"] = en, ["tr"] = tr, ["fr"] = fr, ["es"] = es, ["ar"] = ar, ["ru"] = ru
    };

    private static readonly (Dictionary<string, string> Name, string Hex)[] Lips =
    [
        (Name("Terracotta Nude", "Terrakota Nude", "Nude terre cuite", "Nude terracota", "نود تراكوتا",
            "Терракотовый нюд"), "#B5635A"),
        (Name("Rosewood Satin", "Gül Ağacı Saten", "Satiné bois de rose", "Satinado palo de rosa",
            "ساتان خشب الورد", "Атласное розовое дерево"), "#9C5A50"),
        (Name("Spiced Clay", "Baharatlı Kil", "Argile épicée", "Arcilla especiada", "طين متبل",
            "Пряная глина"), "#8C4A3E"),
        (Name("Coral Blush", "Mercan Allık", "Corail rosé", "Coral rubor", "مرجاني وردي",
            "Коралловый румянец"), "#C57B6B"),
        (Name("Berry Brick", "Kırmızı Tuğla", "Brique baie", "Ladrillo baya", "طوبي توتي",
            "Ягодный кирпич"), "#A85248"),
        (Name("Soft Blush", "Yumuşak Allık", "Rose doux", "Rubor suave", "وردي ناعم",
            "Мягкий румянец"), "#CE9080")
    ];

    private static readonly (Dictionary<string, string> Name, string Hex)[] Cheeks =
    [
        (Name("Peach Cream", "Şeftali Krem", "Crème pêche", "Crema durazno", "خوخي كريمي",
            "Персиковый крем"), "#D99A86"),
        (Name("Apricot Flush", "Kayısı Allık", "Rougeur abricot", "Rubor albaricoque", "توهج المشمش",
            "Абрикосовый румянец"), "#E0A98F"),
        (Name("Soft Brick", "Yumuşak Tuğla", "Brique douce", "Ladrillo suave", "طوبي ناعم",
            "Мягкий кирпич"), "#C98072"),
        (Name("Warm Nude", "Sıcak Nude", "Nude chaud", "Nude cálido", "نود دافئ", "Тёплый нюд"), "#E8B79E"),
        (Name("Clay Rose", "Kil Gül", "Rose argile", "Rosa arcilla", "وردي طيني", "Глиняная роза"), "#B06B5C")
    ];

    private static readonly (Dictionary<string, string> Name, string Hex)[] Eyes =
    [
        (Name("Champagne Shimmer", "Şampanya Parıltısı", "Scintillement champagne", "Brillo champán",
            "لمعان الشمبانيا", "Мерцающее шампанское"), "#C9A46A"),
        (Name("Sand Glow", "Kum Işıltısı", "Éclat sable", "Brillo arena", "توهج رملي", "Песочное сияние"),
            "#E2CA9B"),
        (Name("Warm Rosewood", "Sıcak Gül Ağacı", "Bois de rose chaud", "Palo de rosa cálido",
            "خشب الورد الدافئ", "Тёплое розовое дерево"), "#9C7C72"),
        (Name("Cocoa Matte", "Kakao Mat", "Mat cacao", "Mate cacao", "كاكاو مطفي", "Матовое какао"),
            "#6E4A3C"),
        (Name("Soft Taupe", "Yumuşak Taş Rengi", "Taupe doux", "Topo suave", "بني رمادي ناعم",
            "Мягкий тауп"), "#B58E84")
    ];
}
