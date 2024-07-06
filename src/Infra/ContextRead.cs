using Domain.ClientAggregate;
using MongoDB.Driver;

namespace Infra;

public class ContextRead(IMongoDatabase database)
{
    private readonly IMongoDatabase _database = database;

    public IMongoCollection<ClientRead> Clientes => _database.GetCollection<ClientRead>("Clients");
}