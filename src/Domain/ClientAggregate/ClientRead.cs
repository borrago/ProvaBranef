using Core.DomainObjects;

namespace Domain.ClientAggregate;

public class ClientRead : ReadEntity
{
    public string NomeEmpresa { get; set; } = string.Empty;
    public string Porte { get; set; } = string.Empty;
}
