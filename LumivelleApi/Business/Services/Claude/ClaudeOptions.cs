namespace Business.Services.Claude;

public class ClaudeOptions
{
    public string ApiKey { get; set; }
    public string Model { get; set; } = "claude-sonnet-4-6";
    public string BaseUrl { get; set; } = "https://api.anthropic.com";
    public int MaxTokens { get; set; } = 1024;
}
