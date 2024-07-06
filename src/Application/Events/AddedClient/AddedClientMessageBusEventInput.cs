using Core.MessageBus;

namespace Application.Events.AddedClient;

public class AddedClientMessageBusEventInput(Guid id) : IMessageBusEventInput
{
    public Guid Id { get; } = id;
}
