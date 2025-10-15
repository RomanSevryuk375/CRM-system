using system.Core.Models;
using system.DataAccess.Repositories;

namespace system.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientsRepository _clientsRepository;

    public ClientService(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task<List<Client>> GetAllClients()
    {
        return await _clientsRepository.Get();
    }

    public async Task<Guid> CreteClient(Client client)
    {
        return await _clientsRepository.Create(client);
    }
} 
