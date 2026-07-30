using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;

namespace Business.Services.AiServices;

public static class RemoteConfigApiKeyProvider
{
    private const string ServerNamespace = "firebase-server";
    private static readonly string[] Scopes = ["https://www.googleapis.com/auth/firebase.remoteconfig"];

    public static async Task<Dictionary<string, string>> GetParametersAsync(
        string serviceAccountPath, params string[] parameterKeys)
    {
        if (!File.Exists(serviceAccountPath))
            throw new ApplicationException($"Firebase service account file not found at '{serviceAccountPath}'");

        var serviceAccountJson = await File.ReadAllTextAsync(serviceAccountPath);
        using var serviceAccountDoc = JsonDocument.Parse(serviceAccountJson);
        if (!serviceAccountDoc.RootElement.TryGetProperty("project_id", out var projectIdElement))
            throw new ApplicationException($"'project_id' missing from '{serviceAccountPath}'");
        if (!serviceAccountDoc.RootElement.TryGetProperty("client_email", out var clientEmailElement))
            throw new ApplicationException($"'client_email' missing from '{serviceAccountPath}'");
        if (!serviceAccountDoc.RootElement.TryGetProperty("private_key", out var privateKeyElement))
            throw new ApplicationException($"'private_key' missing from '{serviceAccountPath}'");
        var projectId = projectIdElement.GetString();

        // ServiceAccountCredential defaults to minting a self-signed JWT (aud = the scope string)
        // when only scopes are supplied. The Remote Config REST API rejects that and requires a
        // real OAuth2 access token from Google's token endpoint, so UseJwtAccessWithScopes must be
        // disabled to force the token-exchange flow.
        var credential = new ServiceAccountCredential(
            new ServiceAccountCredential.Initializer(clientEmailElement.GetString())
            {
                Scopes = Scopes,
                UseJwtAccessWithScopes = false
            }.FromPrivateKey(privateKeyElement.GetString()));

        // Google.Apis.FirebaseRemoteConfig.v1 has no typed client for the "Remote Config for
        // servers" namespace, so the raw endpoint is called directly. The credential is still
        // wired in the same way every generated Google Apis client does it: as an HTTP message
        // interceptor via HttpClientFactory, so it attaches/refreshes the bearer token itself
        // instead of us managing the Authorization header by hand.
        using var httpClient = new HttpClientFactory().CreateHttpClient(new CreateHttpClientArgs
        {
            ApplicationName = "LumivelleApi",
            Initializers = { credential }
        });

        var response = await httpClient.GetAsync(
            $"https://firebaseremoteconfig.googleapis.com/v1/projects/{projectId}/namespaces/{ServerNamespace}/remoteConfig");

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(
                $"Firebase Remote Config request failed with {(int)response.StatusCode}: {body}");
        }

        using var remoteConfigDoc = JsonDocument.Parse(await response.Content.ReadAsStreamAsync());
        if (!remoteConfigDoc.RootElement.TryGetProperty("parameters", out var parametersElement))
            throw new ApplicationException(
                $"Firebase Remote Config template for project '{projectId}' has no parameters configured. " +
                $"Add {string.Join(", ", parameterKeys.Select(k => $"'{k}'"))} in the Firebase console.");

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
