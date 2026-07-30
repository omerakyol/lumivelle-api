using System;
using System.Threading;
using System.Threading.Tasks;
using Anthropic.SDK;
using Anthropic.SDK.Messaging;
using Microsoft.Extensions.Configuration;

namespace Business.Services.AiServices;

public class ClaudeService(AnthropicClient anthropicClient, IConfiguration configuration) : IAiService
{
    public async Task<string> AnalyzeImageAsync(byte[] imageBytes, string mediaType, string systemPrompt,
        string userPrompt, CancellationToken cancellationToken)
    {
        var options = configuration.GetSection("ClaudeOptions").Get<AiServiceOptions>() ?? new AiServiceOptions();

        var (compressedBytes, compressedMediaType) =
            await AiImageCompressor.CompressAsync(imageBytes, cancellationToken);

        var parameters = new MessageParameters
        {
            Model = options.Model,
            MaxTokens = options.MaxTokens,
            System = [new SystemMessage(systemPrompt)],
            Messages =
            [
                new Message
                {
                    Role = RoleType.User,
                    Content =
                    [
                        new ImageContent
                        {
                            Source = new ImageSource
                            {
                                Type = SourceType.base64,
                                MediaType = compressedMediaType,
                                Data = Convert.ToBase64String(compressedBytes)
                            }
                        },
                        new TextContent { Text = userPrompt }
                    ]
                }
            ]
        };

        var text = await SendAsync(parameters, cancellationToken);
        return text;
    }

    public async Task<string> AnalyzeTextAsync(string systemPrompt, string userPrompt,
        CancellationToken cancellationToken)
    {
        var options = configuration.GetSection("ClaudeOptions").Get<AiServiceOptions>() ?? new AiServiceOptions();

        var parameters = new MessageParameters
        {
            Model = options.Model,
            MaxTokens = options.MaxTokens,
            System = [new SystemMessage(systemPrompt)],
            Messages = [new Message(RoleType.User, userPrompt)]
        };

        var text = await SendAsync(parameters, cancellationToken);
        return text;
    }

    private async Task<string> SendAsync(MessageParameters parameters, CancellationToken cancellationToken)
    {
        try
        {
            var response = await anthropicClient.Messages.GetClaudeMessageAsync(parameters, cancellationToken);
            var text = response.FirstMessage?.Text;
            if (string.IsNullOrWhiteSpace(text)) throw new ApplicationException("Claude returned empty output");
            return text;
        }
        catch (Exception e) when (e is not OperationCanceledException and not ApplicationException)
        {
            Console.WriteLine(e);
            throw new ApplicationException($"Claude API error: {e.Message}", e);
        }
    }
}