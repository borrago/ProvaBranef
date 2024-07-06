using Infra.Repositories.ClientRepository;
using MediatR;

namespace Application.Queries.GetClientQuery;

public class GetClientQueryHandler(IClientReadRepository clientReadRepository) : IRequestHandler<GetClientQueryInput, GetClientQueryResult>
{
    private readonly IClientReadRepository _clientReadRepository = clientReadRepository ?? throw new ArgumentNullException(nameof(clientReadRepository));

    public async Task<GetClientQueryResult> Handle(GetClientQueryInput request, CancellationToken cancellationToken)
    {
        var clients = await _clientReadRepository.GetAllAsync(cancellationToken);

        return new GetClientQueryResult
        {
            Items = clients,
        };

    }
}