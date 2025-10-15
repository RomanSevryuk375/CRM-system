using system.Core.Models;

namespace system.DataAccess.Repositories
{
    public interface IClientsRepository
    {
        Task<int> Create(Client client);
        Task<List<Client>> Get();
    }
}