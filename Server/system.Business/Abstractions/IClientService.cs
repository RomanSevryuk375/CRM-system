using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IClientService
{
    Task<long> CreateClient(Client client);
    Task<long> DeleteClient(long id);
    Task<ClientItem> GetClientById(long id);
    Task<int> GetCountClients(ClientFilter filter);
    Task<List<ClientItem>> GetPagedClients(ClientFilter filter);
    Task<long> UpdateClient(long id, ClientUpdateModel model);
    Task<long> CreateClientWithUser(Client client, User user);
}