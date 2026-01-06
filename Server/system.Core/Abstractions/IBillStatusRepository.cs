using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IBillStatusRepository
{
    Task<List<BillStatusItem>> Get();
    Task<bool> Exists(int id);
}