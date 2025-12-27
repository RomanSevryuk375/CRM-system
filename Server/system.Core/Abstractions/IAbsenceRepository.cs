using CRMSystem.Core.DTOs.Absence;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IAbsenceRepository
{
    Task<int> Create(Absence absence);
    Task<int> Delete(int id);
    Task<int> GetCount(AbsenceFilter filter);
    Task<List<AbsenceItem>> GetPaged(AbsenceFilter filter);
    Task<int> Update(int id, AbsenceUpdateModel model);
    Task<bool> Exists(int id);
    Task<bool> HasOverLap(int workerId, DateOnly start, DateOnly? end, int? excludeId = null);
}