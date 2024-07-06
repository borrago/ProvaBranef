using Core.MessageBus;

namespace Application.Events.DeletedClient;

public class DeletedClientEventInput(Guid id) : IEventInput
{
    public Guid Id { get; } = id;
}
