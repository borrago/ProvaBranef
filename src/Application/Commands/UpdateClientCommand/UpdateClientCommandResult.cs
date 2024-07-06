namespace Application.Commands.UpdateClientCommand;

public class UpdateClientCommandResult(Guid id)
{
    public Guid Id { get; } = id;
}
