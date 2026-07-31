using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Business.Handlers.Recommendations;

/// <summary>
/// One-time seed content for daily-edit recommendations. Replaces a per-request AI call with a
/// combinatorial bank of preset copy, keyed by (season family, undertone, contrast) — the same
/// three signals <see cref="Queries.GetDailyEdit.GetDailyEditQueryHandler"/> already derives from
/// the user's BeautyProfile. 4 families x 3 undertones x 3 contrasts = 36 buckets, 24 presets each
/// (864 total), so a user goes roughly a month before any preset repeats.
/// </summary>
public static class DailyEditPresetSeedData
{
    private static readonly string[] Families = ["Spring", "Summer", "Autumn", "Winter"];
    private static readonly string[] Undertones = ["Warm", "Cool", "Neutral"];
    private static readonly string[] Contrasts = ["Low", "Medium", "High"];

    private const int PresetsPerBucket = 24;

    public static List<DailyEditPresetDocument> All()
    {
        var presets = new List<DailyEditPresetDocument>();

        foreach (var family in Families)
        foreach (var undertone in Undertones)
        foreach (var contrast in Contrasts)
        {
            presets.AddRange(BuildBucket(family, undertone, contrast));
        }

        return presets;
    }

    private static IEnumerable<DailyEditPresetDocument> BuildBucket(string family, string undertone, string contrast)
    {
        var nouns = SeasonNouns[family];
        var mood = SeasonMood[family];
        var contrastWord = ContrastWord[contrast];
        var contrastDesc = ContrastDescriptor[contrast];
        var undertoneAdjective = UndertoneAdjective[undertone];
        var makeupPool = MakeupPool[family];
        var accessoryPool = AccessoryPool[family];

        var result = new List<DailyEditPresetDocument>();
        for (var i = 0; i < PresetsPerBucket; i++)
        {
            var noun = nouns[i % nouns.Length];
            var makeupStart = i % makeupPool.Length;
            var accessoryStart = (i + 11) % accessoryPool.Length;

            result.Add(new DailyEditPresetDocument
            {
                SeasonFamily = family,
                Undertone = undertone,
                Contrast = contrast,
                SortOrder = i,
                DailyEditTitle = $"{contrastWord} {noun}",
                DailyEditSubtitle = BuildSubtitle(i, family, mood, undertone, contrast, contrastDesc, noun),
                Description = $"Today's light is {mood.ToLowerInvariant()} — Lumi leaned into " +
                               $"{noun.ToLowerInvariant()} to keep your {undertoneAdjective} glow {contrastDesc}.",
                MakeupRecs = TakeWrapping(makeupPool, makeupStart, 3),
                AccessoryTitles = TakeWrapping(accessoryPool, accessoryStart, 3)
            });
        }

        return result;
    }

    /// <summary>
    /// Rotates through 6 differently-worded subtitle templates so presets within the same bucket
    /// don't all read identically — only the title/description varied before this.
    /// </summary>
    private static string BuildSubtitle(
        int index, string family, string mood, string undertone, string contrast, string contrastDesc, string noun)
    {
        var undertoneLower = undertone.ToLowerInvariant();
        var contrastLower = contrast.ToLowerInvariant();
        var familyLower = family.ToLowerInvariant();
        var nounLower = noun.ToLowerInvariant();

        return (index % 6) switch
        {
            0 => $"{mood} tones, tuned to your {undertoneLower} {contrastLower}-contrast palette",
            1 => $"{Capitalize(contrastDesc)} hues for your {undertoneLower} glow",
            2 => $"{noun} energy, tailored to your {familyLower} palette",
            3 => $"A {undertoneLower}, {contrastLower}-contrast edit inspired by {nounLower}",
            4 => $"{family}-inspired, {contrastLower}-contrast styling for today",
            _ => $"Lumi's {undertoneLower}-toned pick, layered with {nounLower}"
        };
    }

    private static string Capitalize(string value) =>
        string.IsNullOrEmpty(value) ? value : char.ToUpperInvariant(value[0]) + value[1..];

    private static T[] TakeWrapping<T>(T[] pool, int start, int count)
    {
        var taken = new T[count];
        for (var i = 0; i < count; i++)
            taken[i] = pool[(start + i) % pool.Length];
        return taken;
    }

    private static readonly Dictionary<string, string> SeasonMood = new()
    {
        ["Spring"] = "Fresh and bright",
        ["Summer"] = "Soft and cool",
        ["Autumn"] = "Warm and rich",
        ["Winter"] = "Crisp and clear"
    };

    private static readonly Dictionary<string, string> ContrastWord = new()
    {
        ["Low"] = "Soft",
        ["Medium"] = "Balanced",
        ["High"] = "Bold"
    };

    private static readonly Dictionary<string, string> ContrastDescriptor = new()
    {
        ["Low"] = "gentle and easy",
        ["Medium"] = "polished and effortless",
        ["High"] = "striking and defined"
    };

    private static readonly Dictionary<string, string> UndertoneAdjective = new()
    {
        ["Warm"] = "sun-kissed",
        ["Cool"] = "frost-kissed",
        ["Neutral"] = "effortlessly balanced"
    };

    private static readonly Dictionary<string, string[]> SeasonNouns = new()
    {
        ["Spring"] =
        [
            "Citrus Bloom", "Coral Bloom", "Peach Glow", "Clear Light", "Fresh Bloom", "Golden Bloom",
            "Bright Meadow", "Petal Flush", "Morning Dew", "Cherry Blossom", "Honeyed Light", "Spring Meadow",
            "Buttercup Glow", "Blush Petal", "Dewy Bloom", "Apricot Light", "Wildflower Bloom", "Soft Daffodil",
            "Rosewater Glow", "Sunrise Bloom", "Pastel Bloom", "Green Meadow Light", "Lily Glow", "Tulip Blush"
        ],
        ["Summer"] =
        [
            "Dusty Rose", "Slate Blue", "Soft Lavender", "Cool Breeze", "Misty Blue", "Powder Rose",
            "Quiet Mist", "Sea Glass", "Cloud Grey", "Periwinkle Haze", "Soft Fog", "Silver Mist",
            "Powder Blue", "Wisteria Haze", "Cool Dusk", "Misty Lilac", "Soft Steel", "Sea Foam",
            "Cloudy Rose", "Pale Orchid", "Frosted Blue", "Quiet Rain", "Grey Pearl", "Muted Iris"
        ],
        ["Autumn"] =
        [
            "Golden Hour", "Terracotta Warmth", "Amber Glow", "Spiced Clay", "Rust & Olive", "Umber Light",
            "Autumn Ember", "Cinnamon Spice", "Harvest Gold", "Bronze Leaf", "Mahogany Glow", "Chestnut Warmth",
            "Copper Ember", "Toasted Amber", "Burnt Sienna", "Golden Wheat", "Maple Glow", "Ochre Light",
            "Autumn Spice", "Warm Clove", "Russet Glow", "Honeyed Bronze", "Nutmeg Warmth", "Marigold Ember"
        ],
        ["Winter"] =
        [
            "Crisp Contrast", "Jewel Tone", "Frosted Light", "True Black", "Icy Clarity", "Deep Jewel",
            "Winter Frost", "Sapphire Night", "Silver Frost", "Obsidian Shine", "Arctic Clarity", "Midnight Jewel",
            "Onyx Depth", "Frozen Starlight", "Emerald Night", "Platinum Frost", "Winter Slate", "Ruby Jewel",
            "Glacial Light", "Deep Amethyst", "Ink Black", "Crystal Frost", "Steel Jewel", "Polar Clarity"
        ]
    };

    private static readonly Dictionary<string, MakeupRecPresetValue[]> MakeupPool = new()
    {
        ["Spring"] =
        [
            new MakeupRecPresetValue { Title = "Coral flush", Subtitle = "Fresh daytime cheek", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Peach lip", Subtitle = "Soft citrus nude", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Golden shimmer", Subtitle = "Bright morning eye", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Dew glow", Subtitle = "Luminous fresh finish", Icon = "droplet" },
            new MakeupRecPresetValue { Title = "Apricot blush", Subtitle = "Warm-toned cheek", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Sunlit liner", Subtitle = "Light bronze definition", Icon = "star" },
            new MakeupRecPresetValue { Title = "Bloom lip", Subtitle = "Sheer coral wash", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Meadow eye", Subtitle = "Soft green-gold shimmer", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Honey gloss", Subtitle = "Sheer golden lip", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Cherry tint", Subtitle = "Soft pink flush", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Petal shimmer", Subtitle = "Light pink eye wash", Icon = "sparkles" },
            new MakeupRecPresetValue
                { Title = "Dew highlight", Subtitle = "Fresh glass-skin glow", Icon = "droplet" },
            new MakeupRecPresetValue { Title = "Buttercup lid", Subtitle = "Soft yellow-gold wash", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Blush petal lip", Subtitle = "Sheer rosy tint", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Wildflower cheek", Subtitle = "Multi-tone flush", Icon = "flower" },
            new MakeupRecPresetValue
                { Title = "Daffodil shimmer", Subtitle = "Bright golden lid", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Rosewater gloss", Subtitle = "Dewy pink lip", Icon = "droplet" },
            new MakeupRecPresetValue { Title = "Sunrise blush", Subtitle = "Warm peach glow", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Pastel liner", Subtitle = "Soft lilac definition", Icon = "star" },
            new MakeupRecPresetValue
                { Title = "Lily highlight", Subtitle = "Fresh luminous glow", Icon = "droplet" },
            new MakeupRecPresetValue { Title = "Tulip lip", Subtitle = "Bright coral-pink", Icon = "sparkles" },
            new MakeupRecPresetValue
                { Title = "Green meadow liner", Subtitle = "Soft sage definition", Icon = "star" },
            new MakeupRecPresetValue { Title = "Apricot gloss", Subtitle = "Sheer warm shine", Icon = "droplet" },
            new MakeupRecPresetValue
                { Title = "Fresh dew cheek", Subtitle = "Luminous natural flush", Icon = "flower" }
        ],
        ["Summer"] =
        [
            new MakeupRecPresetValue { Title = "Rose nude lip", Subtitle = "Muted daytime rose", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Dusty flush", Subtitle = "Cool-toned cheek", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Lavender shimmer", Subtitle = "Soft cool eye", Icon = "star" },
            new MakeupRecPresetValue { Title = "Misty glow", Subtitle = "Sheer dewy finish", Icon = "droplet" },
            new MakeupRecPresetValue { Title = "Slate liner", Subtitle = "Soft smoky definition", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Powder blush", Subtitle = "Cool pink flush", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Breeze lip", Subtitle = "Sheer mauve tint", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Quiet shimmer", Subtitle = "Muted cool highlight", Icon = "star" },
            new MakeupRecPresetValue { Title = "Grey-blue liner", Subtitle = "Soft smoky definition", Icon = "star" },
            new MakeupRecPresetValue { Title = "Periwinkle wash", Subtitle = "Cool lid shimmer", Icon = "sparkles" },
            new MakeupRecPresetValue
                { Title = "Sea glass shimmer", Subtitle = "Iridescent cool highlight", Icon = "droplet" },
            new MakeupRecPresetValue
                { Title = "Cloud blush", Subtitle = "Barely-there cool flush", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Wisteria lid", Subtitle = "Soft purple-grey wash", Icon = "star" },
            new MakeupRecPresetValue { Title = "Steel liner", Subtitle = "Cool grey definition", Icon = "droplet" },
            new MakeupRecPresetValue
                { Title = "Sea foam highlight", Subtitle = "Cool iridescent glow", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Cloudy rose lip", Subtitle = "Muted mauve tint", Icon = "sparkles" },
            new MakeupRecPresetValue
                { Title = "Pale orchid shimmer", Subtitle = "Soft lavender-pink eye", Icon = "star" },
            new MakeupRecPresetValue
                { Title = "Frosted blue liner", Subtitle = "Cool subtle definition", Icon = "droplet" },
            new MakeupRecPresetValue
                { Title = "Quiet rain blush", Subtitle = "Soft grey-toned cheek", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Pearl highlight", Subtitle = "Cool luminous glow", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Muted iris lid", Subtitle = "Cool violet wash", Icon = "star" },
            new MakeupRecPresetValue
                { Title = "Chambray liner", Subtitle = "Soft denim-blue tone", Icon = "droplet" },
            new MakeupRecPresetValue
                { Title = "Dove grey shimmer", Subtitle = "Quiet cool highlight", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Powder lilac lip", Subtitle = "Sheer cool tint", Icon = "flower" }
        ],
        ["Autumn"] =
        [
            new MakeupRecPresetValue { Title = "Terracotta lip", Subtitle = "Warm nude · daytime", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Golden glow", Subtitle = "Champagne eye", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Peach flush", Subtitle = "Cream blush", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Amber shimmer", Subtitle = "Bronze eye look", Icon = "star" },
            new MakeupRecPresetValue { Title = "Spiced clay lip", Subtitle = "Deep matte finish", Icon = "droplet" },
            new MakeupRecPresetValue { Title = "Warm bronze", Subtitle = "Sun-kissed cheeks", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Rust liner", Subtitle = "Smoky warm eye", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Olive accent", Subtitle = "Earthy eye tone", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Cinnamon lip", Subtitle = "Spiced warm nude", Icon = "sparkles" },
            new MakeupRecPresetValue
                { Title = "Bronze leaf shimmer", Subtitle = "Metallic warm eye", Icon = "star" },
            new MakeupRecPresetValue
                { Title = "Mahogany liner", Subtitle = "Deep warm definition", Icon = "droplet" },
            new MakeupRecPresetValue
                { Title = "Chestnut blush", Subtitle = "Warm terracotta cheek", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Copper ember lid", Subtitle = "Warm metallic shimmer", Icon = "sun" },
            new MakeupRecPresetValue
                { Title = "Toasted amber lip", Subtitle = "Warm caramel nude", Icon = "sparkles" },
            new MakeupRecPresetValue
                { Title = "Burnt sienna liner", Subtitle = "Deep warm definition", Icon = "droplet" },
            new MakeupRecPresetValue
                { Title = "Golden wheat blush", Subtitle = "Warm sun-touched cheek", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Maple shimmer", Subtitle = "Warm bronze eye", Icon = "star" },
            new MakeupRecPresetValue { Title = "Ochre lid wash", Subtitle = "Earthy warm tone", Icon = "sun" },
            new MakeupRecPresetValue
                { Title = "Warm clove liner", Subtitle = "Spiced brown definition", Icon = "droplet" },
            new MakeupRecPresetValue { Title = "Russet lip", Subtitle = "Deep warm red-brown", Icon = "sparkles" },
            new MakeupRecPresetValue
                { Title = "Honeyed bronze cheek", Subtitle = "Warm golden flush", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Nutmeg shimmer", Subtitle = "Warm spiced eye tone", Icon = "star" },
            new MakeupRecPresetValue { Title = "Marigold highlight", Subtitle = "Warm golden glow", Icon = "sun" },
            new MakeupRecPresetValue
                { Title = "Sienna gloss", Subtitle = "Warm terracotta shine", Icon = "droplet" }
        ],
        ["Winter"] =
        [
            new MakeupRecPresetValue { Title = "True red lip", Subtitle = "Bold classic finish", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Icy highlight", Subtitle = "Crisp cool shimmer", Icon = "star" },
            new MakeupRecPresetValue { Title = "Jewel eye", Subtitle = "Deep sapphire tone", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Frosted flush", Subtitle = "Cool-toned cheek", Icon = "flower" },
            new MakeupRecPresetValue { Title = "Onyx liner", Subtitle = "Sharp defined line", Icon = "droplet" },
            new MakeupRecPresetValue { Title = "Berry lip", Subtitle = "Deep cool berry", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Platinum shimmer", Subtitle = "Icy highlight tone", Icon = "star" },
            new MakeupRecPresetValue { Title = "Charcoal smoke", Subtitle = "Dramatic winter eye", Icon = "sun" },
            new MakeupRecPresetValue
                { Title = "Sapphire liner", Subtitle = "Deep jewel definition", Icon = "star" },
            new MakeupRecPresetValue
                { Title = "Silver frost shimmer", Subtitle = "Icy metallic highlight", Icon = "sparkles" },
            new MakeupRecPresetValue
                { Title = "Obsidian lash", Subtitle = "Sharp dramatic finish", Icon = "droplet" },
            new MakeupRecPresetValue
                { Title = "Midnight plum lip", Subtitle = "Deep cool berry-plum", Icon = "sun" },
            new MakeupRecPresetValue
                { Title = "Onyx depth liner", Subtitle = "Deep sharp definition", Icon = "droplet" },
            new MakeupRecPresetValue
                { Title = "Starlight highlight", Subtitle = "Icy luminous glow", Icon = "sparkles" },
            new MakeupRecPresetValue
                { Title = "Emerald liner", Subtitle = "Deep jewel-green definition", Icon = "star" },
            new MakeupRecPresetValue { Title = "Platinum lid", Subtitle = "Cool metallic shimmer", Icon = "sun" },
            new MakeupRecPresetValue { Title = "Slate smoke eye", Subtitle = "Cool dramatic finish", Icon = "droplet" },
            new MakeupRecPresetValue { Title = "Ruby lip", Subtitle = "Deep jewel red", Icon = "sparkles" },
            new MakeupRecPresetValue { Title = "Glacial highlight", Subtitle = "Icy cool glow", Icon = "star" },
            new MakeupRecPresetValue
                { Title = "Amethyst liner", Subtitle = "Deep purple definition", Icon = "droplet" },
            new MakeupRecPresetValue { Title = "Ink black lash", Subtitle = "Sharp defined lash line", Icon = "sun" },
            new MakeupRecPresetValue
                { Title = "Crystal shimmer", Subtitle = "Icy metallic highlight", Icon = "sparkles" },
            new MakeupRecPresetValue
                { Title = "Steel jewel liner", Subtitle = "Cool metallic definition", Icon = "star" },
            new MakeupRecPresetValue { Title = "Polar lip", Subtitle = "Cool frosted pink", Icon = "droplet" }
        ]
    };

    private static readonly Dictionary<string, string[]> AccessoryPool = new()
    {
        ["Spring"] =
        [
            "Pearl studs", "Woven tote", "Floral scarf", "Gold bangle", "Straw hat", "Pastel headband",
            "Rose gold hoops", "Canvas bag", "Daisy hairpin", "Coral bracelet", "Linen sundress scarf",
            "Butter-yellow tote", "Daisy chain necklace", "Blush ribbon bow", "Woven straw clutch",
            "Pastel enamel ring", "Cherry blossom clip", "Butter-yellow scarf", "Coral drop earrings",
            "Wicker sun hat", "Rose quartz pendant", "Linen headscarf", "Petal-pink bracelet", "Gold vine ring"
        ],
        ["Summer"] =
        [
            "Silver hoops", "Denim jacket", "Linen scarf", "Blue enamel ring", "Cotton tote", "Pearl bracelet",
            "Chambray wrap", "Seaglass pendant", "Freshwater pearl necklace", "Grey cashmere scarf",
            "Silver anklet", "Sea glass earrings", "Denim bucket hat", "Seashell necklace", "Cotton headscarf",
            "Blue topaz ring", "Woven raffia bag", "Powder blue scarf", "Silver shell earrings",
            "Linen wrap belt", "Grey felt hat", "Pearl anklet", "Chambray headband", "Sea glass ring"
        ],
        ["Autumn"] =
        [
            "Gold hoops", "Silk scarf", "Leather tote", "Amber studs", "Suede belt", "Tortoiseshell clip",
            "Copper cuff", "Wool wrap", "Chestnut leather boots", "Woven basket bag", "Brass hoop earrings",
            "Camel wool scarf", "Suede ankle boots", "Amber pendant necklace", "Woven wool scarf",
            "Bronze cuff bracelet", "Leather crossbody bag", "Copper drop earrings", "Camel felt hat",
            "Tortoiseshell sunglasses", "Rust knit beret", "Brass ring stack", "Wool plaid scarf",
            "Chestnut leather belt"
        ],
        ["Winter"] =
        [
            "Silver studs", "Velvet scarf", "Black leather gloves", "Onyx ring", "Wool beret",
            "Crystal drop earrings", "Faux fur stole", "Platinum cuff", "Sterling silver cuff",
            "Black satin ribbon", "Diamond stud earrings", "Charcoal wool scarf", "Cashmere wrap scarf",
            "Pearl drop earrings", "Black velvet headband", "Silver snowflake pendant", "Faux fur earmuffs",
            "Onyx cufflinks", "Charcoal wool gloves", "Crystal hair pin", "Platinum chain necklace",
            "Velvet clutch bag", "Sterling hoop earrings", "Deep plum scarf"
        ]
    };
}
