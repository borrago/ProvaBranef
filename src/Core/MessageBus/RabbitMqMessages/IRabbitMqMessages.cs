using RabbitMQ.Client;

namespace Core.MessageBus.RabbitMqMessages;

public interface IRabbitMqMessages : IDisposable
{
    public IDictionary<string, IModel> Channels { get; }

    void CreateChannelsSubscriber();

    string GetQueueName(string input);

    void Publish<T>(T message) where T : IMessageBusEventInput;
}