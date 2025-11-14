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

    public async Task<List<Client>> GetPagedClients(int page, int limit)
    {
        var clients = await _clientsRepository.Get();

        var pagedClients = clients
                                .Skip((page - 1) * limit)
                                .Take(limit)
                                .ToList();

        return pagedClients;
    }

    public async Task<int> GetClientCount()
    {
        return await _clientsRepository.GetCount();
    }

    public async Task<List<Client>> GetClientByUserId(int userId)
    {
        return await _clientsRepository.GetClientByUserId(userId);
    }

    public async Task<int> CreateClient(Client client)
    {
        return await _clientsRepository.Create(client);
    }

    public async Task<int> UpdateClient(int id, string name, string surname, string phoneNumber, string email)
    {
        return await _clientsRepository.Update(id, name, surname, phoneNumber, email);
    }

    public async Task<int> DeleteClient(int id)
    {
        return await _clientsRepository.Delete(id);
    }
}
