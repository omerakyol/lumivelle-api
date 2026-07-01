using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Core.Utilities.MessageBrokers.RabbitMq;

public class MqQueueHelper : IMessageBrokerHelper
{
    private readonly MessageBrokerOptions _brokerOptions;

    public MqQueueHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _brokerOptions = Configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
    }

    public IConfiguration Configuration { get; }

    public void QueueMessage(string messageText)
    {
        var factory = new ConnectionFactory();

        factory.UserName = _brokerOptions.UserName;
        factory.Password = _brokerOptions.Password;
        factory.HostName = _brokerOptions.HostName;

        using var connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        using var channel = connection.CreateChannelAsync().GetAwaiter().GetResult();

        channel.QueueDeclareAsync(
            "DArchQueue",
            false,
            false,
            false,
            null).GetAwaiter().GetResult();

        var message = JsonConvert.SerializeObject(messageText);
        var messageBodyBytes = Encoding.UTF8.GetBytes(message);

        channel.BasicPublishAsync(string.Empty, "DArchQueue", false, messageBodyBytes).GetAwaiter().GetResult();
    }
}