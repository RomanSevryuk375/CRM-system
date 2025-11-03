using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IWorkerService
    {
        Task<int> CreateWorker(Worker worker);
        Task<int> DeleteWorker(int id);
        Task<List<Worker>> GetAllWorkers();
        Task<List<WorkerWithInfoDto>> GetWorkersWithInfo();
        Task<int> UpdateWorker(int id, int? userId, int? specialization, string name, string Surname, decimal? hourlyRate, string phoneNumber, string email);
    }
}