using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IAbsenceTypeService
{
    Task<int> CreateAbsenceType(AbsenceType absenceType);
    Task<int> DeleteAbsenceType(int id);
    Task<List<AbsenceTypeItem>> GetAllAbsenceType();
    Task<int> UpdateAbsenceType(int id, string name);
}