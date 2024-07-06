using Application.Events.AddedClient;
using Core.MessageBus.RabbitMqMessages;
using Domain.ClientAggregate;
using Infra.Repositories.ClientRepository;
using Newtonsoft.Json;

namespace Application.Subscribers;

public class AddedClientSubscriber(IClientReadRepository clientReadRepository, IClientRepository clientRepository) : IRabbitMqSubrscriber
{
    private readonly IClientReadRepository _clientReadRepository = clientReadRepository ?? throw new ArgumentNullException(nameof(clientReadRepository));
    private readonly IClientRepository _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));

    public async Task Handle(string message, CancellationToken cancellationToken)
    {
        var req = JsonConvert.DeserializeObject<AddedClientEventInput>(message) ?? throw new Exception("Erro ao ler a mensagem.");

        var client = await _clientRepository.GetAsNoTrackingAsync(g => g.Id == req.Id, cancellationToken) ?? throw new Exception("Erro ao buscar cliente.");

        var clientRead = new ClientRead
        {
            Id = client.Id,
            NomeEmpresa = client.Nome,
            Porte = client.Porte.ToString()
        };

        var inserted = await _clientReadRepository.InsertAsync(clientRead, cancellationToken);
        if (!inserted)
            throw new Exception("Erro ao inserir o cliente no banco de leitura.");
    }
}
