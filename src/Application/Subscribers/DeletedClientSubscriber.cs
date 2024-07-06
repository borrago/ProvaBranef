using Application.Events.DeletedClient;
using Core.MessageBus.RabbitMqMessages;
using Infra.Repositories.ClientRepository;
using Newtonsoft.Json;

namespace Application.Subscribers;

public class DeletedClientSubscriber(IClientReadRepository clientReadRepository) : IRabbitMqSubrscriber
{
    private readonly IClientReadRepository _clientReadRepository = clientReadRepository ?? throw new ArgumentNullException(nameof(clientReadRepository));

    public async Task Handle(string message, CancellationToken cancellationToken)
    {
        var req = JsonConvert.DeserializeObject<DeletedClientEventInput>(message) ?? throw new Exception("Erro ao ler a mensagem.");

        var removed = await _clientReadRepository.RemoveAsync(req.Id, cancellationToken);
        if (!removed)
            throw new Exception("Erro ao remover o cliente no banco de leitura.");
    }
}
