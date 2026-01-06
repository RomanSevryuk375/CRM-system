using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ISpecializationRepository
{
    Task<int> Create(Specialization specialization);
    Task<int> Delete(int id);
    Task<List<SpecializationItem>> Get();
    Task<int> Update(int id, string? name);
    Task<bool> Exists(int id);
    Task<bool> ExistsByName(string name);
}