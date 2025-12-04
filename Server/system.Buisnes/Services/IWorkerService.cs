using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IWorkerService
    {
        Task<int> CreateWorker(Worker worker);
        Task<int> DeleteWorker(int id);
        Task<List<Worker>> GetPagedAllWorkers(int page, int limit);
        Task<int> GetCountWorker();
        Task<List<WorkerWithInfoDto>> GetPagedWorkerByUserId(int userId, int page, int limit);
        Task<int> GetCountWorkerByUserId(int userId);
        Task<List<WorkerWithInfoDto>> GetPagedWorkersWithInfo(int page, int limit);
        Task<int> UpdateWorker(int id, int? userId, int? specialization, string? name, string? Surname, decimal? hourlyRate, string? phoneNumber, string? email);
    }
}