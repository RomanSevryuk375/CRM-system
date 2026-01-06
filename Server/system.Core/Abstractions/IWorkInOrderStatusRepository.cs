using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IWorkInOrderStatusRepository
{
    Task<List<WorkInOrderStatusItem>> Get();
    Task<bool> Exists(int id);
}