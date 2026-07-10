using System.Threading;
using System.Threading.Tasks;

namespace Business.Services.Claude;

public interface IClaudeVisionService
{
    Task<string> AnalyzeImageAsync(
        byte[] imageBytes,
        string mediaType,
        string systemPrompt,
        string userPrompt,
        CancellationToken cancellationToken);
}
