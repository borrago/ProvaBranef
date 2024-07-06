using Core.MessageBus;
using MediatR;

namespace Application.Events.DeletedClient;

public class DeletedClientEventHandler(IMessageBus messageBus) : INotificationHandler<DeletedClientEventInput>
{
    private readonly IMessageBus _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));

    public async Task Handle(DeletedClientEventInput @event, CancellationToken cancellationToken)
    {
        var brokerEvent = new DeletedClientMessageBusEventInput(@event.Id);
        await _messageBus.PublishAsync(brokerEvent, cancellationToken);
    }
}
