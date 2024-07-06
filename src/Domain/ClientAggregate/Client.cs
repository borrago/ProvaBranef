using Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace Domain.ClientAggregate;

public class Client : Entity, IAggregateRoot
{
    // EF Core
    protected Client()
    {
    }

    public Client(string nome, Porte porte)
    {
        WithNome(nome);
        WithPorte(porte);
    }

    public void WithNome(string nomeEmpresa)
    {
        Validations.ValidarSeVazio(nomeEmpresa, "O campo NomeEmpresa não pode estar vazio.");
        Validations.ValidarSeNulo(nomeEmpresa, "O campo NomeEmpresa não pode ser nulo.");
        Validations.ValidarTamanho(nomeEmpresa, 250, "O campo NomeEmpresa não pode ser maior que 250 caracteres.");

        Nome = nomeEmpresa;
    }

    public void WithPorte(Porte porte)
    {
        Validations.ValidarSeNulo(porte, "O campo Porte não pode ser nulo.");

        Porte = porte;
    }

    [MaxLength(250)]
    public string Nome { get; private set; } = string.Empty;
    public Porte Porte { get; private set; }
}