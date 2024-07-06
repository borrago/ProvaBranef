using Application.Events.AddedClient;
using Domain.ClientAggregate;
using Infra.Repositories.ClientRepository;
using MediatR;
namespace Application.Commands.AddClientCommand;

public class AddClientCommandHandler(IClientRepository clientRepository, IMediator mediator) : IRequestHandler<AddClientCommandInput, AddClientCommandResult>
{
    private readonly IClientRepository _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<AddClientCommandResult> Handle(AddClientCommandInput request, CancellationToken cancellationToken)
    {
        var client = new Client(request.Nome, request.Porte);
        await _clientRepository.AddAsync(client, cancellationToken);
        await _clientRepository.UnitOfWork.CommitAsync(cancellationToken);

        var @event = new AddedClientEventInput(client.Id);
        await _mediator.Publish(@event, cancellationToken);

        return new AddClientCommandResult(client.Id);
    }
}