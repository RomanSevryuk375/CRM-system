using CRMSystem.Core.DTOs.Client;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IClientsRepository
    {
        Task<long> Create(Client client);
        Task<long> Delete(long id);
        Task<int> GetCount(ClientFilter filter);
        Task<List<ClientItem>> GetPaged(ClientFilter filter);
        Task<long> Update(long id, ClientUpdateModel model);
    }
}