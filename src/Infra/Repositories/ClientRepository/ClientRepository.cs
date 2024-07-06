using Core.Infra;
using Domain.ClientAggregate;

namespace Infra.Repositories.ClientRepository;

public class ClientRepository(Context context) : GenericRepository<Client>(context), IClientRepository
{
}