namespace Application.Commands.DeleteClientCommand;

public class DeleteClientCommandResult(Guid id)
{
    public Guid Id { get; } = id;
}
