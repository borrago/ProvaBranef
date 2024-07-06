using Core.MessageBus;

namespace Application.Events.UpdatedClient;

public class UpdatedClientEventInput(Guid id) : IEventInput
{
    public Guid Id { get; } = id;
}
