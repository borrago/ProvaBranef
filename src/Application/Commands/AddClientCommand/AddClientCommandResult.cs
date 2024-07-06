namespace Application.Commands.AddClientCommand;

public class AddClientCommandResult(Guid id)
{
    public Guid Id { get; } = id;
}
