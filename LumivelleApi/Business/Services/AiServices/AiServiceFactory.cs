using System;

namespace Business.Services.AiServices;

public class AiServiceFactory(OpenAiService openAi) : IAiServiceFactory
{
    public IAiService Get(AiProvider provider)
    {
        return provider switch
        {
            AiProvider.OpenAi => openAi,
            _ => throw new NotSupportedException($"Provider {provider} is not supported")
        };
    }
}