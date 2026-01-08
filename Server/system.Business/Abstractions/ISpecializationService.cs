using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ISpecializationService
{
    Task<int> CreateSpecialization(Specialization specialization, CancellationToken ct);
    Task<int> DeleteSpecialization(int id, CancellationToken ct);
    Task<List<SpecializationItem>> GetSpecializations(CancellationToken ct);
    Task<int> UpdateSpecialization(int id, string? name, CancellationToken ct);
}