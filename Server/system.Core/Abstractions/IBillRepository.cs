using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IBillRepository
    {
        Task<List<Bill>> Get();

        Task<int> Create(Bill bill);
    }
}