using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IClientsRepository
{
    Task<int> Create(Client client);
    Task<List<Client>> Get();
}