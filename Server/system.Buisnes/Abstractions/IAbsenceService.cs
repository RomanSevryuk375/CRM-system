using CRMSystem.Core.DTOs.Absence;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IAbsenceService
{
    Task<int> CreateAbsence(Absence absence);
    Task<int> DeleteAbsence(int id);
    Task<int> GetCountAbsence(AbsenceFilter filter);
    Task<List<AbsenceItem>> GetPagedAbsence(AbsenceFilter filter);
    Task<int> UpdateAbsence(AbsenceUpdateModel model);
}