using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IAbsenceTypeService
{
    Task<int> CretaeAbsence(AbsenceType absenceType);
    Task<int> DeleteAbsence(int id);
    Task<List<AbsenceTypeItem>> GetPagedAbsence(int Page, int Limit);
    Task<int> UpdateAbsence(int id, string name);
}