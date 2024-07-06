using Domain.ClientAggregate;

namespace Application.Queries.GetClientQuery;

public class GetClientQueryResult
{
    public IEnumerable<ClientRead> Items { get; set; } = null!;
}
