using CRMSystem.Core.ProjectionModels.Absence;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IAbsenceService
{
    Task<int> CreateAbsence(Absence absence, CancellationToken ct);
    Task<int> DeleteAbsence(int id, CancellationToken ct);
    Task<int> GetCountAbsence(AbsenceFilter filter, CancellationToken ct);
    Task<List<AbsenceItem>> GetPagedAbsence(AbsenceFilter filter, CancellationToken ct);
    Task<int> UpdateAbsence(int id, AbsenceUpdateModel model, CancellationToken ct);
}