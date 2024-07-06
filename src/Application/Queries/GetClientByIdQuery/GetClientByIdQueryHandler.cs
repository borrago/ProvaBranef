using Infra.Repositories.ClientRepository;
using MediatR;

namespace Application.Queries.GetClientByIdQuery;

public class GetClientByIdQueryHandler(IClientReadRepository clientReadRepository) : IRequestHandler<GetClientByIdQueryInput, GetClientByIdQueryResult>
{
    private readonly IClientReadRepository _clientReadRepository = clientReadRepository ?? throw new ArgumentNullException(nameof(clientReadRepository));

    public async Task<GetClientByIdQueryResult> Handle(GetClientByIdQueryInput request, CancellationToken cancellationToken)
    {
        var client = await _clientReadRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new Exception("Cliente não localizado.");

        return new GetClientByIdQueryResult
        {
            Id = client.Id,
            NomeEmpresa = client.NomeEmpresa,
            Porte = client.Porte,
        };

    }
}