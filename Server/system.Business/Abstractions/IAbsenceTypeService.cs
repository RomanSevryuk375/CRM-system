using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IAbsenceTypeService
{
    Task<int> CreateAbsenceType(AbsenceType absenceType, CancellationToken ct);
    Task<int> DeleteAbsenceType(int id, CancellationToken ct);
    Task<List<AbsenceTypeItem>> GetAllAbsenceType(CancellationToken ct);
    Task<int> UpdateAbsenceType(int id, string name, CancellationToken ct);
}