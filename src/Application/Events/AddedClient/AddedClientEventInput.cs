using Core.MessageBus;

namespace Application.Events.AddedClient;

public class AddedClientEventInput(Guid id) : IEventInput
{
    public Guid Id { get; } = id;
}
