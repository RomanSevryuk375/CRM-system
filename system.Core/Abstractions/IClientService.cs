using system.Core.Models;

namespace system.Buisnes.Services
{
    public interface IClientService
    {
        Task<int> CeateClient(Client client);
        Task<List<Client>> GetClients();
    }
}