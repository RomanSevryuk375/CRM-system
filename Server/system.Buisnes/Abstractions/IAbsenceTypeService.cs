using CRMSystem.Core.DTOs.AbsenceType;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IAbsenceTypeService
{
    Task<int> CretaeAbsenceType(AbsenceType absenceType);
    Task<int> DeleteAbsenceType(int id);
    Task<List<AbsenceTypeItem>> GetAllAbsenceType();
    Task<int> UpdateAbsenceType(int id, string name);
}