using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IWorkerRepository
{
    Task<List<Worker>> Get();
    Task<List<Worker>> GetPaged(int page, int limit);
    Task<int> GetCount();
    Task<List<Worker>> GetWorkerByUserId(int userId);
    Task<List<Worker>> GetPagedWorkerByUserId(int userId, int page, int limit);
    Task<int> GetCountWorkerByUserId(int userId);
    Task<int> Create(Worker worker);
    Task<int> Delete(int id);
    Task<int> Update(int id, int? userId, int? specialization, string? name, string? Surname, decimal? hourlyRate, string? phoneNumber, string? email);
}