using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IWorkerRepository
    {
        Task<int> Create(Worker worker);
        Task<int> Delete(int id);
        Task<List<Worker>> Get();
        Task<int> Update(int id, int? userId, int? specialization, string name, string Surname, decimal? hourlyRate, string phoneNumber, string email);
    }
}