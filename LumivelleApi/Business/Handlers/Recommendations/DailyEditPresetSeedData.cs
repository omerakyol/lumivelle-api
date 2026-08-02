using System;
using System.Collections.Generic;
using Business.Handlers.Recommendations.WordBanks;
using Core.Entities.Concrete;

namespace Business.Handlers.Recommendations;

/// <summary>
/// One-time seed content for daily-edit recommendations. Replaces a per-request AI call with a
/// combinatorial bank of preset copy, keyed by (season family, undertone, contrast) — the same
/// three signals <see cref="Queries.GetDailyEdit.GetDailyEditQueryHandler"/> already derives from
/// the user's BeautyProfile. 4 families x 3 undertones x 3 contrasts = 36 buckets, 24 presets each
/// (864 total), so a user goes roughly a month before any preset repeats.
///
/// Every user-facing field is a language-keyed dictionary (see <see cref="SupportedLanguages"/>),
/// built by composing per-language word banks under <c>WordBanks/</c> — one file per language. Each
/// <c>*PresetWordBanks</c> class owns its own vocabulary AND its own sentence composition
/// (<c>BuildTitle</c>/<c>BuildSubtitle</c>/<c>BuildDescription</c>), so it can look up whatever
/// grammatically-inflected word forms it needs (gender/case agreement differs per language and per
/// grammatical slot) instead of the generator forcing one pre-resolved string through every language.
/// </summary>
public static class DailyEditPresetSeedData
{
    public static readonly string[] SupportedLanguages = ["en", "tr", "fr", "es", "ar", "ru"];

    private static readonly string[] Families = ["Spring", "Summer", "Autumn", "Winter"];
    private static readonly string[] Undertones = ["Warm", "Cool", "Neutral"];
    private static readonly string[] Contrasts = ["Low", "Medium", "High"];

    private const int PresetsPerBucket = 24;

    private static readonly Dictionary<string, Dictionary<string, string[]>> SeasonNouns = new()
    {
        ["en"] = EnPresetWordBanks.SeasonNouns,
        ["tr"] = TrPresetWordBanks.SeasonNouns,
        ["fr"] = FrPresetWordBanks.SeasonNouns,
        ["es"] = EsPresetWordBanks.SeasonNouns,
        ["ar"] = ArPresetWordBanks.SeasonNouns,
        ["ru"] = RuPresetWordBanks.SeasonNouns
    };

    private static readonly Dictionary<string, Dictionary<string, string[]>> MakeupTitles = new()
    {
        ["en"] = EnPresetWordBanks.MakeupTitles,
        ["tr"] = TrPresetWordBanks.MakeupTitles,
        ["fr"] = FrPresetWordBanks.MakeupTitles,
        ["es"] = EsPresetWordBanks.MakeupTitles,
        ["ar"] = ArPresetWordBanks.MakeupTitles,
        ["ru"] = RuPresetWordBanks.MakeupTitles
    };

    private static readonly Dictionary<string, Dictionary<string, string[]>> MakeupSubtitles = new()
    {
        ["en"] = EnPresetWordBanks.MakeupSubtitles,
        ["tr"] = TrPresetWordBanks.MakeupSubtitles,
        ["fr"] = FrPresetWordBanks.MakeupSubtitles,
        ["es"] = EsPresetWordBanks.MakeupSubtitles,
        ["ar"] = ArPresetWordBanks.MakeupSubtitles,
        ["ru"] = RuPresetWordBanks.MakeupSubtitles
    };

    // Icons are a controlled vocabulary (sanitized again on read in GetDailyEditQueryHandler), not
    // translatable — kept as a single language-agnostic pool, indexed positionally against MakeupTitles.
    private static readonly Dictionary<string, string[]> MakeupIcons = EnPresetWordBanks.MakeupIcons;

    private static readonly Dictionary<string, Dictionary<string, string[]>> AccessoryPool = new()
    {
        ["en"] = EnPresetWordBanks.AccessoryPool,
        ["tr"] = TrPresetWordBanks.AccessoryPool,
        ["fr"] = FrPresetWordBanks.AccessoryPool,
        ["es"] = EsPresetWordBanks.AccessoryPool,
        ["ar"] = ArPresetWordBanks.AccessoryPool,
        ["ru"] = RuPresetWordBanks.AccessoryPool
    };

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
        var nounCount = SeasonNouns["en"][family].Length;
        var makeupCount = MakeupTitles["en"][family].Length;
        var accessoryCount = AccessoryPool["en"][family].Length;

        var result = new List<DailyEditPresetDocument>();
        for (var i = 0; i < PresetsPerBucket; i++)
        {
            var makeupStart = i % makeupCount;
            var accessoryStart = (i + 11) % accessoryCount;

            var title = new Dictionary<string, string>();
            var subtitle = new Dictionary<string, string>();
            var description = new Dictionary<string, string>();

            foreach (var lang in SupportedLanguages)
            {
                var noun = SeasonNouns[lang][family][i % nounCount];

                title[lang] = BuildTitle(lang, contrast, noun);
                subtitle[lang] = BuildSubtitle(lang, i % 6, family, undertone, contrast, noun);
                description[lang] = BuildDescription(lang, family, undertone, contrast, noun);
            }

            var makeupRecs = new MakeupRecPresetValue[3];
            for (var k = 0; k < 3; k++)
            {
                var idx = (makeupStart + k) % makeupCount;
                var mkTitle = new Dictionary<string, string>();
                var mkSubtitle = new Dictionary<string, string>();
                foreach (var lang in SupportedLanguages)
                {
                    mkTitle[lang] = MakeupTitles[lang][family][idx];
                    mkSubtitle[lang] = MakeupSubtitles[lang][family][idx];
                }

                makeupRecs[k] = new MakeupRecPresetValue
                {
                    Title = mkTitle,
                    Subtitle = mkSubtitle,
                    Icon = MakeupIcons[family][idx]
                };
            }

            var accessoryTitles = new List<Dictionary<string, string>>();
            for (var k = 0; k < 3; k++)
            {
                var idx = (accessoryStart + k) % accessoryCount;
                var dict = new Dictionary<string, string>();
                foreach (var lang in SupportedLanguages)
                    dict[lang] = AccessoryPool[lang][family][idx];
                accessoryTitles.Add(dict);
            }

            result.Add(new DailyEditPresetDocument
            {
                SeasonFamily = family,
                Undertone = undertone,
                Contrast = contrast,
                SortOrder = i,
                DailyEditTitle = title,
                DailyEditSubtitle = subtitle,
                Description = description,
                MakeupRecs = makeupRecs,
                AccessoryTitles = accessoryTitles
            });
        }

        return result;
    }

    private static string BuildTitle(string lang, string contrast, string noun) => lang switch
    {
        "en" => EnPresetWordBanks.BuildTitle(contrast, noun),
        "tr" => TrPresetWordBanks.BuildTitle(contrast, noun),
        "fr" => FrPresetWordBanks.BuildTitle(contrast, noun),
        "es" => EsPresetWordBanks.BuildTitle(contrast, noun),
        "ar" => ArPresetWordBanks.BuildTitle(contrast, noun),
        "ru" => RuPresetWordBanks.BuildTitle(contrast, noun),
        _ => throw new ArgumentOutOfRangeException(nameof(lang), lang, "Unsupported language code")
    };

    private static string BuildSubtitle(string lang, int variant, string family, string undertone, string contrast, string noun) =>
        lang switch
        {
            "en" => EnPresetWordBanks.BuildSubtitle(variant, family, undertone, contrast, noun),
            "tr" => TrPresetWordBanks.BuildSubtitle(variant, family, undertone, contrast, noun),
            "fr" => FrPresetWordBanks.BuildSubtitle(variant, family, undertone, contrast, noun),
            "es" => EsPresetWordBanks.BuildSubtitle(variant, family, undertone, contrast, noun),
            "ar" => ArPresetWordBanks.BuildSubtitle(variant, family, undertone, contrast, noun),
            "ru" => RuPresetWordBanks.BuildSubtitle(variant, family, undertone, contrast, noun),
            _ => throw new ArgumentOutOfRangeException(nameof(lang), lang, "Unsupported language code")
        };

    private static string BuildDescription(string lang, string family, string undertone, string contrast, string noun) =>
        lang switch
        {
            "en" => EnPresetWordBanks.BuildDescription(family, undertone, contrast, noun),
            "tr" => TrPresetWordBanks.BuildDescription(family, undertone, contrast, noun),
            "fr" => FrPresetWordBanks.BuildDescription(family, undertone, contrast, noun),
            "es" => EsPresetWordBanks.BuildDescription(family, undertone, contrast, noun),
            "ar" => ArPresetWordBanks.BuildDescription(family, undertone, contrast, noun),
            "ru" => RuPresetWordBanks.BuildDescription(family, undertone, contrast, noun),
            _ => throw new ArgumentOutOfRangeException(nameof(lang), lang, "Unsupported language code")
        };
}
