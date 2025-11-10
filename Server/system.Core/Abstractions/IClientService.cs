using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IClientService
{
    Task<int> CreateClient(Client client);
    Task<List<Client>> GetClients();
    Task<List<Client>> GetClientByUserId(int userId);
    Task<int> UpdateClient(int id, string name, string surname, string phoneNumber, string email);
    Task<int> DeleteClient(int id);
}