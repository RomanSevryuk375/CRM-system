using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IAbsenceTypeRepository
{
    Task<int> Create(AbsenceType absenceType);
    Task<int> Delete(int id);
    Task<List<AbsenceTypeItem>> GetAll();
    Task<List<AbsenceTypeItem>> GetByName(string name);
    Task<int> Update(int id, string name);
}