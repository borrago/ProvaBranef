using Core.MessageBus;
using MediatR;

namespace Application.Events.UpdatedClient;

internal class UpdatedClientEventHandler(IMessageBus messageBus) : INotificationHandler<UpdatedClientEventInput>
{
    private readonly IMessageBus _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));

    public async Task Handle(UpdatedClientEventInput @event, CancellationToken cancellationToken)
    {
        var brokerEvent = new UpdatedClientMessageBusEventInput(@event.Id);
        await _messageBus.PublishAsync(brokerEvent, cancellationToken);
    }
}
