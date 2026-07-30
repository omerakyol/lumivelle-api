using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Responses;

namespace Business.Services.AiServices;

public class OpenAiService(OpenAIClient openAiClient, IConfiguration configuration) : IAiService
{
    public async Task<string> AnalyzeImageAsync(byte[] imageBytes, string mediaType, string systemPrompt,
        string userPrompt, CancellationToken cancellationToken)
    {
        var options = configuration.GetSection("OpenAiOptions").Get<AiServiceOptions>() ?? new AiServiceOptions();

        var (compressedBytes, compressedMediaType) =
            await AiImageCompressor.CompressAsync(imageBytes, cancellationToken);

        var imagePart =
            ResponseContentPart.CreateInputImagePart(BinaryData.FromBytes(compressedBytes, compressedMediaType));
        var textPart = ResponseContentPart.CreateInputTextPart(userPrompt);

        var response = await openAiClient.GetResponsesClient().CreateResponseAsync(
            new CreateResponseOptions
            {
                Model = options.Model,
                MaxOutputTokenCount = options.MaxTokens,
                Instructions = systemPrompt,
                InputItems =
                {
                    ResponseItem.CreateUserMessageItem([textPart, imagePart])
                }
            },
            cancellationToken);

        var text = response.Value.GetOutputText();

        if (string.IsNullOrWhiteSpace(text)) throw new ApplicationException("OpenAI returned empty output");

        return text;
    }

    public async Task<string> AnalyzeTextAsync(string systemPrompt, string userPrompt,
        CancellationToken cancellationToken)
    {
        var options = configuration.GetSection("OpenAiOptions").Get<AiServiceOptions>() ?? new AiServiceOptions();
        var response = await openAiClient.GetResponsesClient().CreateResponseAsync(
            new CreateResponseOptions
            {
                Model = options.Model, MaxOutputTokenCount = options.MaxTokens, Instructions = systemPrompt,
                InputItems = { ResponseItem.CreateUserMessageItem(userPrompt) }
            }, cancellationToken);

        var text = response.Value.GetOutputText();

        if (string.IsNullOrWhiteSpace(text)) throw new ApplicationException("OpenAI returned empty output");

        return text;
    }
}