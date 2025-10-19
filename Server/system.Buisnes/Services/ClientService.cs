using CRMSystem.Core.Models;
using CRMSystem.Core.Abstractions;

namespace CRMSystem.Buisnes.Services;

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

    public async Task<int> CreateClient(Client client)
    {
        return await _clientsRepository.Create(client);
    }
}
