using Core.MessageBus;

namespace Application.Events.DeletedClient;

public class DeletedClientMessageBusEventInput(Guid id) : IMessageBusEventInput
{
    public Guid Id { get; } = id;
}
