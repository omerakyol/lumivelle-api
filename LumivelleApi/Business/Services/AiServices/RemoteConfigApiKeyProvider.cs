using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;

namespace Business.Services.AiServices;

public static class RemoteConfigApiKeyProvider
{
    private static readonly string[] Scopes = ["https://www.googleapis.com/auth/firebase.remoteconfig.readonly"];

    public static async Task<Dictionary<string, string>> GetParametersAsync(
        string serviceAccountPath, params string[] parameterKeys)
    {
        if (!File.Exists(serviceAccountPath))
            throw new ApplicationException($"Firebase service account file not found at '{serviceAccountPath}'");

        var serviceAccountJson = await File.ReadAllTextAsync(serviceAccountPath);
        using var serviceAccountDoc = JsonDocument.Parse(serviceAccountJson);
        if (!serviceAccountDoc.RootElement.TryGetProperty("project_id", out var projectIdElement))
            throw new ApplicationException($"'project_id' missing from '{serviceAccountPath}'");
        var projectId = projectIdElement.GetString();

        var credential = GoogleCredential.FromFile(serviceAccountPath).CreateScoped(Scopes);
        var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await httpClient.GetAsync(
            $"https://firebaseremoteconfig.googleapis.com/v1/projects/{projectId}/remoteConfig");

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(
                $"Firebase Remote Config request failed with {(int)response.StatusCode}: {body}");
        }

        using var remoteConfigDoc = JsonDocument.Parse(await response.Content.ReadAsStreamAsync());
        if (!remoteConfigDoc.RootElement.TryGetProperty("parameters", out var parametersElement))
            throw new ApplicationException("Firebase Remote Config response has no 'parameters' object");

        var result = new Dictionary<string, string>();
        foreach (var key in parameterKeys)
        {
            if (!parametersElement.TryGetProperty(key, out var parameterElement) ||
                !parameterElement.TryGetProperty("defaultValue", out var defaultValueElement) ||
                !defaultValueElement.TryGetProperty("value", out var valueElement) ||
                string.IsNullOrWhiteSpace(valueElement.GetString()))
            {
                throw new ApplicationException(
                    $"Firebase Remote Config parameter '{key}' is missing or has no default value");
            }

            result[key] = valueElement.GetString()!;
        }

        return result;
    }
}
