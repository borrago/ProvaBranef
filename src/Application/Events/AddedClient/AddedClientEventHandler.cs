using Core.MessageBus;
using MediatR;

namespace Application.Events.AddedClient;

public class AddedClientEventHandler(IMessageBus messageBus) : INotificationHandler<AddedClientEventInput>
{
    private readonly IMessageBus _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));

    public async Task Handle(AddedClientEventInput @event, CancellationToken cancellationToken)
    {
        var brokerEvent = new AddedClientMessageBusEventInput(@event.Id);
        await _messageBus.PublishAsync(brokerEvent, cancellationToken);
    }
}
