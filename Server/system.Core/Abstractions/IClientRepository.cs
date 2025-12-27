using CRMSystem.Core.DTOs.Client;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IClientRepository
{
    Task<long> Create(Client client);
    Task<long> Delete(long id);
    Task<int> GetCount(ClientFilter filter);
    Task<List<ClientItem>> GetPaged(ClientFilter filter);
    Task<ClientItem?> GetById(long id);
    Task<long> Update(long id, ClientUpdateModel model);
    Task<bool> Exists(long id);
}