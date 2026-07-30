# Design: Source Claude/OpenAI API Keys from Firebase Remote Config

## Goal

`ClaudeOptions:ApiKey` and `OpenAiOptions:ApiKey` currently come from `IConfiguration`
(appsettings/user-secrets/env vars). They should instead be fetched from Firebase Remote
Config, using parameter keys `ClaudeApiKey` and `OpenAiApiKey`.

## Context

- `AiServiceOptions` (`Business/Services/AiServices/AiServiceOptions.cs`) is bound from both
  the `"ClaudeOptions"` and `"OpenAiOptions"` sections; it holds `ApiKey`, `Model`, `BaseUrl`,
  `MaxTokens`.
- `Business/Startup.cs` builds `AnthropicClient`/`OpenAIClient` as singletons once at
  `ConfigureServices` time, using `options.ApiKey` read from `IConfiguration`.
- `ClaudeService`/`OpenAiService` additionally re-read `ApiKey` from `IConfiguration` on every
  call, purely to null-check it and throw `ApplicationException` if missing.
- FirebaseAdmin 3.6.0 (already referenced) has no Remote Config client — only Auth/Messaging.
  Remote Config must be fetched via a raw REST call to the Remote Config v1 REST API,
  authenticated with a service-account OAuth2 token.
- A Firebase service-account credential file already exists and is used for push notifications:
  `WebAPI/wwwroot/firebase-config.json` (`Business/Helpers/FirebaseNotificationService.cs`).
  This design reuses that same file — no new credential or appsettings section is introduced.

## Design

### New component: `RemoteConfigApiKeyProvider`

`Business/Services/AiServices/RemoteConfigApiKeyProvider.cs`

```csharp
public static class RemoteConfigApiKeyProvider
{
    public static async Task<Dictionary<string, string>> GetParametersAsync(
        string serviceAccountPath, params string[] parameterKeys)
}
```

Behavior:
1. Read `project_id` out of the service-account JSON at `serviceAccountPath`.
2. Build a scoped credential: `GoogleCredential.FromFile(serviceAccountPath)
   .CreateScoped("https://www.googleapis.com/auth/firebase.remoteconfig.readonly")` and get a
   bearer token via `GetAccessTokenForRequestAsync()`.
3. `GET https://firebaseremoteconfig.googleapis.com/v1/projects/{project_id}/remoteConfig`
   with that bearer token.
4. Parse the JSON body; for each requested key read
   `parameters.<key>.defaultValue.value`.
5. If the HTTP call fails, the body doesn't parse, or any requested key is missing/blank,
   throw `ApplicationException` with a message naming the missing key — this aborts startup
   (fail-fast, no fallback to appsettings).

### Startup wiring (`Business/Startup.cs`)

- Before the two `services.AddSingleton(p => new AnthropicClient/OpenAIClient(...))`
  registrations, resolve the service-account path via
  `((IWebHostEnvironment)hostEnvironment).WebRootPath` + `"firebase-config.json"` (same path
  `FirebaseNotificationService` uses).
- Call `RemoteConfigApiKeyProvider.GetParametersAsync(path, "ClaudeApiKey", "OpenAiApiKey")`
  once, synchronously, via `.GetAwaiter().GetResult()` (single blocking call at startup,
  consistent with the existing "build singleton once" pattern already used for these clients).
- Use the returned `ClaudeApiKey`/`OpenAiApiKey` values directly when constructing
  `AnthropicClient`/`OpenAIClient`, instead of `options.ApiKey`.
- `Model`/`BaseUrl`/`MaxTokens` continue to come from the `ClaudeOptions`/`OpenAiOptions`
  appsettings sections, unchanged.

### Cleanup (`ClaudeService.cs`, `OpenAiService.cs`)

- Remove the per-call `if (string.IsNullOrWhiteSpace(options.ApiKey)) throw ...` blocks in both
  `AnalyzeImageAsync`/`AnalyzeTextAsync`. `ApiKey` no longer lives in `IConfiguration` at all, so
  this check would always fail; startup already guarantees a valid key exists (fail-fast), so the
  per-call check is redundant.

## Error handling

- Any failure fetching/parsing Remote Config, or a missing/blank parameter value, throws during
  `ConfigureServices` and aborts application startup. No silent fallback.

## Out of scope

- No caching/refresh of the keys while the app is running — a key rotation in Remote Config
  requires an app restart (accepted trade-off, matches existing "build once" pattern).
- No changes to `Model`/`BaseUrl`/`MaxTokens` sourcing.
- No new appsettings section — the existing `firebase-config.json` service account is reused.

## Testing

- No test project exists in this solution (per `CLAUDE.md`). Verification is manual: run
  `dotnet build`, then start the API and confirm it boots successfully and an AI analysis
  endpoint (Claude and OpenAI) succeeds, pulling keys from the Remote Config values
  `ClaudeApiKey`/`OpenAiApiKey`.
