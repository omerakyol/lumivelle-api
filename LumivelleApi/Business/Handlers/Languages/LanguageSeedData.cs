using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Business.Handlers.Languages;

/// <summary>
/// One-time seed content for the supported-languages lookup collection (<see cref="Language"/>,
/// Mongo collection "languages"). <see cref="Language.Code"/> matches the 2-letter codes the
/// mobile app's i18next setup and <c>Account.Language</c> already use.
/// </summary>
public static class LanguageSeedData
{
    public static List<Language> All() =>
    [
        new Language { Name = "English", Code = "en" },
        new Language { Name = "Türkçe", Code = "tr" },
        new Language { Name = "Français", Code = "fr" },
        new Language { Name = "Español", Code = "es" },
        new Language { Name = "العربية", Code = "ar" },
        new Language { Name = "Русский", Code = "ru" }
    ];
}
