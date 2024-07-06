using Domain.ClientAggregate;
using MediatR;

namespace Application.Commands.UpdateClientCommand;

public class UpdateClientCommandInput(Guid id, string nome, Porte porte) : IRequest<UpdateClientCommandResult>
{
    public Guid Id { get; } = id;
    public string Nome { get; } = nome;
    public Porte Porte { get; } = porte;
}
