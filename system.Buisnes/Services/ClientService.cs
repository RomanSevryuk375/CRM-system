using system.Core.Models;
using system.DataAccess.Repositories;

namespace system.Buisnes.Services;

public class ClientService : IClientService
{
    private readonly IClientsRepository _clientsRepository;

    public ClientService(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task<List<Client>> GetClients()
    {
        return await _clientsRepository.Get();
    }

    public async Task<int> CeateClient(Client client)
    {
        return await _clientsRepository.Create(client);
    }
}
