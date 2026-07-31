namespace Business.Services.AiServices;

public class AiServiceOptions
{
    public string ApiKey { get; set; }
    public string Model { get; set; } = "gpt-5-mini";
    public string BaseUrl { get; set; } = "https://api.openai.com";
    public int MaxTokens { get; set; } = 1024;
}
