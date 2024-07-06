using Core.MessageBus;

namespace Application.Events.UpdatedClient;

public class UpdatedClientMessageBusEventInput(Guid id) : IMessageBusEventInput
{
    public Guid Id { get; } = id;
}
