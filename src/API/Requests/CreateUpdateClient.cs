using Domain.ClientAggregate;

namespace API.Requests;

public class CreateUpdateClient
{
    public string Nome { get; set; } = string.Empty;
    public Porte Porte { get; set; }
}
