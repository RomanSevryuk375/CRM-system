using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IAbsenceTypeRepository
{
    Task<int> Create(AbsenceType absenceType, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<List<AbsenceTypeItem>> GetAll(CancellationToken ct);
    Task<List<AbsenceTypeItem>> GetByName(string name, CancellationToken ct);
    Task<int> Update(int id, string name, CancellationToken ct);
}