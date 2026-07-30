using System;

namespace Business.Services.AiServices;

public class AiServiceFactory(OpenAiService openAi, ClaudeService claude) : IAiServiceFactory
{
    public IAiService Get(AiProvider provider)
    {
        return provider switch
        {
            AiProvider.OpenAi => openAi, AiProvider.Claude => claude,
            _ => throw new NotSupportedException($"Provider {provider} is not supported")
        };
    }
}