using Application.Events.AddedClient;
using Application.Events.DeletedClient;
using Infra.Repositories.ClientRepository;
using MediatR;

namespace Application.Commands.DeleteClientCommand;

internal class DeleteClientCommandHandler(IClientRepository clientRepository, IMediator mediator) : IRequestHandler<DeleteClientCommandInput, DeleteClientCommandResult>
{
    private readonly IClientRepository _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<DeleteClientCommandResult> Handle(DeleteClientCommandInput request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetAsync(g => g.Id == request.Id, cancellationToken) ?? throw new Exception("Cliente não encontrado.");

        _clientRepository.Remove(client);
        await _clientRepository.UnitOfWork.CommitAsync(cancellationToken);

        var @event = new DeletedClientEventInput(client.Id);
        await _mediator.Publish(@event, cancellationToken);

        return new DeleteClientCommandResult(client.Id);
    }
}