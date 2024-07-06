using MediatR;

namespace Application.Commands.DeleteClientCommand;

public class DeleteClientCommandInput(Guid id) : IRequest<DeleteClientCommandResult>
{
    public Guid Id { get; } = id;
}
