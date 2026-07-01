using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Business.Helpers;

/// <summary>
/// Helper service for parsing and processing Accept-Language header from HTTP requests
/// </summary>
public interface ILanguageHelper
{
    string GetAcceptLanguage(string fallbackLanguage = "en", Dictionary<string, string>? replacements = null);
    string GetAcceptLanguageWithDefaultRules();
}

public class LanguageHelper(IHttpContextAccessor httpContextAccessor) : ILanguageHelper
{
    /// <summary>
    /// Parses Accept-Language header from HTTP request and cleans it
    /// </summary>
    /// <param name="fallbackLanguage">Default language to use if none found (default: "en")</param>
    /// <param name="replacements">Dictionary for replacing specific languages (e.g., tr->en)</param>
    /// <returns>Parsed language code</returns>
    public string GetAcceptLanguage(
        string fallbackLanguage = "en",
        Dictionary<string, string>? replacements = null)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext == null)
            return fallbackLanguage;

        var acceptLanguage = httpContext.Request.Headers.AcceptLanguage.FirstOrDefault();

        if (string.IsNullOrWhiteSpace(acceptLanguage))
            return fallbackLanguage;

        // Remove quality value (;q=0.9)
        if (acceptLanguage.Contains(';'))
        {
            acceptLanguage = acceptLanguage.Split(';')[0];
        }

        // Get first language if multiple exist (en-US,en;q=0.9)
        if (acceptLanguage.Contains(','))
        {
            acceptLanguage = acceptLanguage.Split(',')[0];
        }

        // Trim whitespace
        acceptLanguage = acceptLanguage.Trim();

        // Apply language replacement rules
        if (replacements != null && replacements.ContainsKey(acceptLanguage))
        {
            acceptLanguage = replacements[acceptLanguage];
        }

        return acceptLanguage;
    }

    /// <summary>
    /// Gets language code with default rules (tr->en)
    /// </summary>
    public string GetAcceptLanguageWithDefaultRules()
    {
        var replacements = new Dictionary<string, string>
        {
            { "tr", "en" }
        };

        return GetAcceptLanguage("en", replacements);
    }
}