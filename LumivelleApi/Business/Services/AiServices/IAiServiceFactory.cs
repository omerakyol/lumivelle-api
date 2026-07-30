namespace Business.Services.AiServices;

public interface IAiServiceFactory
{
    IAiService Get(AiProvider provider);
}