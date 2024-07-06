using Core.Infra;
using Domain.ClientAggregate;
using MongoDB.Driver;

namespace Infra.Repositories.ClientRepository;

public class ClientReadRepository(IMongoDatabase database) : GenericReadRepository<ClientRead>(database, "clients"), IClientReadRepository
{
}