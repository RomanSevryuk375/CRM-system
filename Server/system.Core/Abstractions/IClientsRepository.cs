using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IClientsRepository
{
    Task<int> Create(Client client);

    Task<List<Client>> Get();

    Task<int> Update(int id, string name, string surname, string phoneNumber, string email);

    Task<int> Delete(int id);

    Task<int> GetClientIdByUserId(int userId);
}