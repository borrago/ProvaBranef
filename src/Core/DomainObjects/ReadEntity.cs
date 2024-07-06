namespace Core.DomainObjects;

public abstract class ReadEntity : IReadEntity
{
    public Guid Id { get; set; }
}
