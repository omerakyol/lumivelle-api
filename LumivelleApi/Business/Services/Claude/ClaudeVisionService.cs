using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Business.Services.Claude;

public class ClaudeVisionService(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration) : IClaudeVisionService
{
    public async Task<string> AnalyzeImageAsync(
        byte[] imageBytes,
        string mediaType,
        string systemPrompt,
        string userPrompt,
        CancellationToken cancellationToken)
    {
        var options = configuration.GetSection("ClaudeOptions").Get<ClaudeOptions>() ?? new ClaudeOptions();
        if (string.IsNullOrEmpty(options.ApiKey))
            throw new ApplicationException("ClaudeOptions:ApiKey is not configured");

        var payload = new
        {
            model = options.Model,
            max_tokens = options.MaxTokens,
            system = systemPrompt,
            messages = new object[]
            {
                new
                {
                    role = "user",
                    content = new object[]
                    {
                        new
                        {
                            type = "image",
                            source = new
                            {
                                type = "base64",
                                media_type = mediaType,
                                data = Convert.ToBase64String(imageBytes)
                            }
                        },
                        new { type = "text", text = userPrompt }
                    }
                }
            }
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{options.BaseUrl}/v1/messages");
        request.Headers.Add("x-api-key", options.ApiKey);
        request.Headers.Add("anthropic-version", "2023-06-01");
        request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var client = httpClientFactory.CreateClient("claude");
        var response = await client.SendAsync(request, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Claude API error {(int)response.StatusCode}: {body}");

        using var document = JsonDocument.Parse(body);
        return document.RootElement.GetProperty("content")[0].GetProperty("text").GetString();
    }

    public async Task<string> AnalyzeTextAsync(
        string systemPrompt,
        string userPrompt,
        CancellationToken cancellationToken)
    {
        var options = configuration.GetSection("ClaudeOptions").Get<ClaudeOptions>() ?? new ClaudeOptions();
        if (string.IsNullOrEmpty(options.ApiKey))
            throw new ApplicationException("ClaudeOptions:ApiKey is not configured");

        var payload = new
        {
            model = options.Model,
            max_tokens = options.MaxTokens,
            system = systemPrompt,
            messages = new object[]
            {
                new { role = "user", content = userPrompt }
            }
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{options.BaseUrl}/v1/messages");
        request.Headers.Add("x-api-key", options.ApiKey);
        request.Headers.Add("anthropic-version", "2023-06-01");
        request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var client = httpClientFactory.CreateClient("claude");
        var response = await client.SendAsync(request, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Claude API error {(int)response.StatusCode}: {body}");

        using var document = JsonDocument.Parse(body);
        return document.RootElement.GetProperty("content")[0].GetProperty("text").GetString();
    }
}
