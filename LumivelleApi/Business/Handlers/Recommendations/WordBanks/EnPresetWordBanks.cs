using System.Collections.Generic;

namespace Business.Handlers.Recommendations.WordBanks;

/// <summary>
/// English word banks for <see cref="DailyEditPresetSeedData"/>. This is the canonical/reference
/// language — every other language's word bank must mirror its family keys and per-family item
/// counts (24 nouns / makeup pairs / accessories per season family) so the generator can compose
/// presets by index across languages.
/// </summary>
internal static class EnPresetWordBanks
{
    public static readonly Dictionary<string, string> SeasonMood = new()
    {
        ["Spring"] = "Fresh and bright",
        ["Summer"] = "Soft and cool",
        ["Autumn"] = "Warm and rich",
        ["Winter"] = "Crisp and clear"
    };

    public static readonly Dictionary<string, string> ContrastWord = new()
    {
        ["Low"] = "Soft",
        ["Medium"] = "Balanced",
        ["High"] = "Bold"
    };

    public static readonly Dictionary<string, string> ContrastDescriptor = new()
    {
        ["Low"] = "gentle and easy",
        ["Medium"] = "polished and effortless",
        ["High"] = "striking and defined"
    };

    public static readonly Dictionary<string, string> UndertoneAdjective = new()
    {
        ["Warm"] = "sun-kissed",
        ["Cool"] = "frost-kissed",
        ["Neutral"] = "effortlessly balanced"
    };

    // Plain lowercase words for mid-sentence use in BuildSubtitle (distinct from UndertoneAdjective's
    // stylistic phrasing and ContrastWord's title-case marketing word).
    public static readonly Dictionary<string, string> UndertoneWord = new()
    {
        ["Warm"] = "warm",
        ["Cool"] = "cool",
        ["Neutral"] = "neutral"
    };

    public static readonly Dictionary<string, string> ContrastLevel = new()
    {
        ["Low"] = "low",
        ["Medium"] = "medium",
        ["High"] = "high"
    };

    // Translated season-family display names ("Spring"/"Autumn"/... in this language) for
    // mid-sentence use in BuildSubtitle, since the SeasonFamily bucket key itself stays English.
    public static readonly Dictionary<string, string> FamilyName = new()
    {
        ["Spring"] = "Spring",
        ["Summer"] = "Summer",
        ["Autumn"] = "Autumn",
        ["Winter"] = "Winter"
    };

    public static readonly Dictionary<string, string[]> SeasonNouns = new()
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

    public static readonly Dictionary<string, string[]> MakeupTitles = new()
    {
        ["Spring"] =
        [
            "Coral flush", "Peach lip", "Golden shimmer", "Dew glow", "Apricot blush", "Sunlit liner",
            "Bloom lip", "Meadow eye", "Honey gloss", "Cherry tint", "Petal shimmer", "Dew highlight",
            "Buttercup lid", "Blush petal lip", "Wildflower cheek", "Daffodil shimmer", "Rosewater gloss",
            "Sunrise blush", "Pastel liner", "Lily highlight", "Tulip lip", "Green meadow liner",
            "Apricot gloss", "Fresh dew cheek"
        ],
        ["Summer"] =
        [
            "Rose nude lip", "Dusty flush", "Lavender shimmer", "Misty glow", "Slate liner", "Powder blush",
            "Breeze lip", "Quiet shimmer", "Grey-blue liner", "Periwinkle wash", "Sea glass shimmer",
            "Cloud blush", "Wisteria lid", "Steel liner", "Sea foam highlight", "Cloudy rose lip",
            "Pale orchid shimmer", "Frosted blue liner", "Quiet rain blush", "Pearl highlight", "Muted iris lid",
            "Chambray liner", "Dove grey shimmer", "Powder lilac lip"
        ],
        ["Autumn"] =
        [
            "Terracotta lip", "Golden glow", "Peach flush", "Amber shimmer", "Spiced clay lip", "Warm bronze",
            "Rust liner", "Olive accent", "Cinnamon lip", "Bronze leaf shimmer", "Mahogany liner",
            "Chestnut blush", "Copper ember lid", "Toasted amber lip", "Burnt sienna liner",
            "Golden wheat blush", "Maple shimmer", "Ochre lid wash", "Warm clove liner", "Russet lip",
            "Honeyed bronze cheek", "Nutmeg shimmer", "Marigold highlight", "Sienna gloss"
        ],
        ["Winter"] =
        [
            "True red lip", "Icy highlight", "Jewel eye", "Frosted flush", "Onyx liner", "Berry lip",
            "Platinum shimmer", "Charcoal smoke", "Sapphire liner", "Silver frost shimmer", "Obsidian lash",
            "Midnight plum lip", "Onyx depth liner", "Starlight highlight", "Emerald liner", "Platinum lid",
            "Slate smoke eye", "Ruby lip", "Glacial highlight", "Amethyst liner", "Ink black lash",
            "Crystal shimmer", "Steel jewel liner", "Polar lip"
        ]
    };

    public static readonly Dictionary<string, string[]> MakeupSubtitles = new()
    {
        ["Spring"] =
        [
            "Fresh daytime cheek", "Soft citrus nude", "Bright morning eye", "Luminous fresh finish",
            "Warm-toned cheek", "Light bronze definition", "Sheer coral wash", "Soft green-gold shimmer",
            "Sheer golden lip", "Soft pink flush", "Light pink eye wash", "Fresh glass-skin glow",
            "Soft yellow-gold wash", "Sheer rosy tint", "Multi-tone flush", "Bright golden lid", "Dewy pink lip",
            "Warm peach glow", "Soft lilac definition", "Fresh luminous glow", "Bright coral-pink",
            "Soft sage definition", "Sheer warm shine", "Luminous natural flush"
        ],
        ["Summer"] =
        [
            "Muted daytime rose", "Cool-toned cheek", "Soft cool eye", "Sheer dewy finish",
            "Soft smoky definition", "Cool pink flush", "Sheer mauve tint", "Muted cool highlight",
            "Soft smoky definition", "Cool lid shimmer", "Iridescent cool highlight", "Barely-there cool flush",
            "Soft purple-grey wash", "Cool grey definition", "Cool iridescent glow", "Muted mauve tint",
            "Soft lavender-pink eye", "Cool subtle definition", "Soft grey-toned cheek", "Cool luminous glow",
            "Cool violet wash", "Soft denim-blue tone", "Quiet cool highlight", "Sheer cool tint"
        ],
        ["Autumn"] =
        [
            "Warm nude · daytime", "Champagne eye", "Cream blush", "Bronze eye look", "Deep matte finish",
            "Sun-kissed cheeks", "Smoky warm eye", "Earthy eye tone", "Spiced warm nude", "Metallic warm eye",
            "Deep warm definition", "Warm terracotta cheek", "Warm metallic shimmer", "Warm caramel nude",
            "Deep warm definition", "Warm sun-touched cheek", "Warm bronze eye", "Earthy warm tone",
            "Spiced brown definition", "Deep warm red-brown", "Warm golden flush", "Warm spiced eye tone",
            "Warm golden glow", "Warm terracotta shine"
        ],
        ["Winter"] =
        [
            "Bold classic finish", "Crisp cool shimmer", "Deep sapphire tone", "Cool-toned cheek",
            "Sharp defined line", "Deep cool berry", "Icy highlight tone", "Dramatic winter eye",
            "Deep jewel definition", "Icy metallic highlight", "Sharp dramatic finish", "Deep cool berry-plum",
            "Deep sharp definition", "Icy luminous glow", "Deep jewel-green definition", "Cool metallic shimmer",
            "Cool dramatic finish", "Deep jewel red", "Icy cool glow", "Deep purple definition",
            "Sharp defined lash line", "Icy metallic highlight", "Cool metallic definition", "Cool frosted pink"
        ]
    };

    public static readonly Dictionary<string, string[]> MakeupIcons = new()
    {
        ["Spring"] =
        [
            "flower", "sparkles", "sun", "droplet", "flower", "star", "sparkles", "sun", "sun", "flower",
            "sparkles", "droplet", "sun", "flower", "flower", "sparkles", "droplet", "sun", "star", "droplet",
            "sparkles", "star", "droplet", "flower"
        ],
        ["Summer"] =
        [
            "sparkles", "flower", "star", "droplet", "sun", "flower", "sparkles", "star", "star", "sparkles",
            "droplet", "flower", "star", "droplet", "sparkles", "sparkles", "star", "droplet", "flower", "sun",
            "star", "droplet", "sparkles", "flower"
        ],
        ["Autumn"] =
        [
            "sparkles", "sun", "flower", "star", "droplet", "sun", "sparkles", "flower", "sparkles", "star",
            "droplet", "flower", "sun", "sparkles", "droplet", "flower", "star", "sun", "droplet", "sparkles",
            "flower", "star", "sun", "droplet"
        ],
        ["Winter"] =
        [
            "sparkles", "star", "sun", "flower", "droplet", "sparkles", "star", "sun", "star", "sparkles",
            "droplet", "sun", "droplet", "sparkles", "star", "sun", "droplet", "sparkles", "star", "droplet",
            "sun", "sparkles", "star", "droplet"
        ]
    };

    public static readonly Dictionary<string, string[]> AccessoryPool = new()
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

    public static string BuildTitle(string contrast, string noun) => $"{ContrastWord[contrast]} {noun}";

    public static string BuildSubtitle(int variant, string family, string undertone, string contrast, string noun)
    {
        var mood = SeasonMood[family];
        var undertoneLower = UndertoneWord[undertone];
        var contrastLower = ContrastLevel[contrast];
        var familyLower = FamilyName[family].ToLowerInvariant();
        var nounLower = noun.ToLowerInvariant();
        var contrastDesc = ContrastDescriptor[contrast];

        return variant switch
        {
            0 => $"{mood} tones, tuned to your {undertoneLower} {contrastLower}-contrast palette",
            1 => $"{Capitalize(contrastDesc)} hues for your {undertoneLower} glow",
            2 => $"{noun} energy, tailored to your {familyLower} palette",
            3 => $"A {undertoneLower}, {contrastLower}-contrast edit inspired by {nounLower}",
            4 => $"{FamilyName[family]}-inspired, {contrastLower}-contrast styling for today",
            _ => $"Lumi's {undertoneLower}-toned pick, layered with {nounLower}"
        };
    }

    public static string BuildDescription(string family, string undertone, string contrast, string noun)
    {
        var moodLower = SeasonMood[family].ToLowerInvariant();
        var nounLower = noun.ToLowerInvariant();
        var undertoneAdjective = UndertoneAdjective[undertone];
        var contrastDesc = ContrastDescriptor[contrast];

        return $"Today's light is {moodLower} — Lumi leaned into {nounLower} to keep your {undertoneAdjective} glow {contrastDesc}.";
    }

    private static string Capitalize(string value) =>
        string.IsNullOrEmpty(value) ? value : char.ToUpperInvariant(value[0]) + value[1..];
}
