using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ISpecializationService
{
    Task<int> CreateSpecialization(Specialization specialization);
    Task<int> DeleteSpecialization(int id);
    Task<List<SpecializationItem>> GetSpecializations();
    Task<int> UpdateSpecialization(int id, string? name);
}