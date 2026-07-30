using System.Threading;
using System.Threading.Tasks;

namespace Business.Services.AiServices;

public interface IAiService
{
    Task<string> AnalyzeImageAsync(
        byte[] imageBytes,
        string mediaType,
        string systemPrompt,
        string userPrompt,
        CancellationToken cancellationToken);

    Task<string> AnalyzeTextAsync(
        string systemPrompt,
        string userPrompt,
        CancellationToken cancellationToken);
}