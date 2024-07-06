using Application.Events.UpdatedClient;
using Infra.Repositories.ClientRepository;
using MediatR;

namespace Application.Commands.UpdateClientCommand;

internal class UpdateClientCommandHandler(IClientRepository clientRepository, IMediator mediator) : IRequestHandler<UpdateClientCommandInput, UpdateClientCommandResult>
{
    private readonly IClientRepository _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<UpdateClientCommandResult> Handle(UpdateClientCommandInput request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetAsync(g => g.Id == request.Id, cancellationToken) ?? throw new Exception("Cliente não encontrado.");

        client.WithNome(request.Nome);
        client.WithPorte(request.Porte);

        _clientRepository.Update(client);
        await _clientRepository.UnitOfWork.CommitAsync(cancellationToken);

        var @event = new UpdatedClientEventInput(client.Id);
        await _mediator.Publish(@event, cancellationToken);

        return new UpdateClientCommandResult(client.Id);
    }
}