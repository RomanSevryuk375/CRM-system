using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IClientService
{
    Task<int> CreateClient(Client client);
    Task<List<Client>> GetClients();
}