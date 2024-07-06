using Domain.ClientAggregate;
using MediatR;

namespace Application.Commands.AddClientCommand;

public class AddClientCommandInput(string nome, Porte porte) : IRequest<AddClientCommandResult>
{
    public string Nome { get; } = nome;
    public Porte Porte { get; } = porte;
}
