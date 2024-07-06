using MediatR;

namespace Application.Queries.GetClientByIdQuery;

public class GetClientByIdQueryInput(Guid id) : IRequest<GetClientByIdQueryResult>
{
    public Guid Id { get; } = id;
}