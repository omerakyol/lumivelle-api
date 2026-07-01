using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Helpers;

public static class PhraseHelper
{
    private static readonly List<string> Phrases =
    [
        "ahead", "palm", "uncle", "grow", "moggy", "media",
        "shiver", "solve", "annual", "zorro", "toward",
        "creek", "amok", "kaput", "quince"
    ];

    public static List<string> GetUniqueRandomPhrases(int count = 15)
    {
        return Phrases
            .OrderBy(x => Guid.NewGuid())
            .Take(count)
            .ToList();
    }
}