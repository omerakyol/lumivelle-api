using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.Utilities.MessageBrokers.RabbitMq;

public class MqConsumerHelper : IMessageConsumer
{
    private readonly IConfiguration _configuration;
    private readonly MessageBrokerOptions _brokerOptions;

    public MqConsumerHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
    }

    public void GetQueue()
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

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (ch, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Message: {message}");
            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        var consumerTag = channel.BasicConsumeAsync("DArchQueue",
            true,
            consumer).GetAwaiter().GetResult();


        Console.ReadKey();
    }
}