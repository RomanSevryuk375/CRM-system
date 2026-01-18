using CRMSystem.Core.ProjectionModels.Absence;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IAbsenceRepository
{
    Task<int> Create(Absence absence, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<int> GetCount(AbsenceFilter filter, CancellationToken ct);
    Task<List<AbsenceItem>> GetPaged(AbsenceFilter filter, CancellationToken ct);
    Task<int> Update(int id, AbsenceUpdateModel model, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
    Task<int?> GetWorkerId(int id, CancellationToken ct);
    Task<List<Absence?>> GetByWorkerId(int workerId, CancellationToken ct);
}