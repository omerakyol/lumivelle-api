# Firebase Remote Config API Keys Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Source the Claude and OpenAI API keys from Firebase Remote Config parameters `ClaudeApiKey`/`OpenAiApiKey` instead of `IConfiguration`, reusing the existing `firebase-config.json` service account.

**Architecture:** A new static helper (`RemoteConfigApiKeyProvider`) fetches Remote Config parameter values via a raw REST call authenticated with the existing Firebase service-account credential. `Business/Startup.cs` calls it once, synchronously, at `ConfigureServices` time, and uses the returned keys to build the `AnthropicClient`/`OpenAIClient` singletons. The now-redundant per-call `ApiKey` null-checks in `ClaudeService`/`OpenAiService` are removed.

**Tech Stack:** .NET 10, `Google.Apis.Auth.OAuth2` (already referenced transitively), `System.Net.Http`, `System.Text.Json`.

## Global Constraints

- Fetch happens once at startup, blocking (`.GetAwaiter().GetResult()`) — no background refresh/caching.
- Fail fast: any fetch/parse error or missing/blank parameter throws and aborts startup — no fallback to appsettings.
- Reuse the existing `WebAPI/wwwroot/firebase-config.json` service account — no new credential file or appsettings section.
- Remote Config parameter keys are exactly `ClaudeApiKey` and `OpenAiApiKey`.
- No test project exists in this solution — verification is `dotnet build` plus a manual run.

---

### Task 1: `RemoteConfigApiKeyProvider` helper

**Files:**
- Create: `Business/Services/AiServices/RemoteConfigApiKeyProvider.cs`

**Interfaces:**
- Produces: `public static class RemoteConfigApiKeyProvider` with
  `public static async Task<Dictionary<string, string>> GetParametersAsync(string serviceAccountPath, params string[] parameterKeys)`
  — returns a dictionary keyed by the requested parameter names; throws `ApplicationException` if the file/HTTP/parse fails or any key is missing/blank.

- [ ] **Step 1: Write the helper**

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
```

- [ ] **Step 2: Build to verify it compiles**

Run: `dotnet build`
Expected: Build succeeds with no errors referencing `RemoteConfigApiKeyProvider`.

- [ ] **Step 3: Commit**

```bash
git add Business/Services/AiServices/RemoteConfigApiKeyProvider.cs
git commit -m "feat: add Firebase Remote Config API key provider"
```

---

### Task 2: Wire the provider into `Business/Startup.cs`

**Files:**
- Modify: `Business/Startup.cs:115-127` (the two `services.AddSingleton(p => new OpenAIClient/AnthropicClient(...))` blocks)

**Interfaces:**
- Consumes: `RemoteConfigApiKeyProvider.GetParametersAsync(string serviceAccountPath, params string[] parameterKeys)` from Task 1.

- [ ] **Step 1: Replace the two singleton registrations**

Replace this block (currently at `Business/Startup.cs:115-127`):

```csharp
        services.AddSingleton(p =>
        {
            var config = p.GetRequiredService<IConfiguration>();
            var options = config.GetSection("OpenAiOptions").Get<AiServiceOptions>()!;
            return new OpenAIClient(options.ApiKey);
        });

        services.AddSingleton(p =>
        {
            var config = p.GetRequiredService<IConfiguration>();
            var options = config.GetSection("ClaudeOptions").Get<AiServiceOptions>()!;
            return new AnthropicClient(options.ApiKey);
        });
```

with:

```csharp
        var firebaseServiceAccountPath = Path.Combine(
            ((IWebHostEnvironment)HostEnvironment).WebRootPath, "firebase-config.json");

        var aiApiKeys = RemoteConfigApiKeyProvider
            .GetParametersAsync(firebaseServiceAccountPath, "ClaudeApiKey", "OpenAiApiKey")
            .GetAwaiter().GetResult();

        services.AddSingleton(_ => new OpenAIClient(aiApiKeys["OpenAiApiKey"]));

        services.AddSingleton(_ => new AnthropicClient(aiApiKeys["ClaudeApiKey"]));
```

Add these two `using` directives at the top of `Business/Startup.cs` if not already present:

```csharp
using System.IO;
using Microsoft.AspNetCore.Hosting;
```

- [ ] **Step 2: Build to verify it compiles**

Run: `dotnet build`
Expected: Build succeeds. If `IWebHostEnvironment` is not resolvable, confirm `Business.csproj` already references `Microsoft.AspNetCore.Hosting.Abstractions` (it does — `FirebaseNotificationService.cs` already uses `IWebHostEnvironment`).

- [ ] **Step 3: Commit**

```bash
git add Business/Startup.cs
git commit -m "feat: fetch Claude/OpenAI API keys from Firebase Remote Config at startup"
```

---

### Task 3: Remove redundant per-call `ApiKey` checks

**Files:**
- Modify: `Business/Services/AiServices/ClaudeService.cs:15-17` and `:56-58`
- Modify: `Business/Services/AiServices/OpenAiService.cs:15-17` and `:49-51`

**Interfaces:**
- Consumes: nothing new — this only deletes now-dead validation code, since `ApiKey` no longer lives in `IConfiguration` and startup (Task 2) already guarantees a valid key exists before any request is served.

- [ ] **Step 1: Remove the check in `ClaudeService.AnalyzeImageAsync`**

In `Business/Services/AiServices/ClaudeService.cs`, delete:

```csharp
        if (string.IsNullOrWhiteSpace(options.ApiKey))
            throw new ApplicationException("ClaudeOptions:ApiKey is not configured");
```

(both occurrences, in `AnalyzeImageAsync` and `AnalyzeTextAsync`). Keep the `var options = configuration.GetSection("ClaudeOptions").Get<AiServiceOptions>() ?? new AiServiceOptions();` line — it's still needed for `Model`/`MaxTokens`.

- [ ] **Step 2: Remove the check in `OpenAiService`**

In `Business/Services/AiServices/OpenAiService.cs`, delete the equivalent two blocks:

```csharp
        if (string.IsNullOrWhiteSpace(options.ApiKey))
            throw new ApplicationException("OpenAiOptions:ApiKey is not configured");
```

- [ ] **Step 3: Build to verify it compiles**

Run: `dotnet build`
Expected: Build succeeds with no unused-variable warnings for `options` (it's still used for `Model`/`MaxTokens`).

- [ ] **Step 4: Commit**

```bash
git add Business/Services/AiServices/ClaudeService.cs Business/Services/AiServices/OpenAiService.cs
git commit -m "refactor: drop redundant ApiKey checks now that startup fetch is fail-fast"
```

---

### Task 4: Manual verification

**Files:** none (verification only)

- [ ] **Step 1: Build the whole solution**

Run: `dotnet build`
Expected: Success, zero errors.

- [ ] **Step 2: Run the API and confirm startup succeeds**

Run: `dotnet watch run --project ./WebAPI/WebAPI.csproj`
Expected: The app starts without an `ApplicationException` about Remote Config — this proves both `ClaudeApiKey` and `OpenAiApiKey` were fetched successfully from Firebase Remote Config (they must already exist as parameters in the Firebase console for the project referenced by `firebase-config.json`).

- [ ] **Step 3: Exercise one Claude and one OpenAI analysis endpoint**

Use whichever existing endpoint routes through `IAiServiceFactory` (e.g. the beauty-profile or wardrobe image analysis endpoint) once with each provider selected, and confirm both return a successful (non-500) response — proving the fetched keys are valid and wired to the right client.
