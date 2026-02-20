using CRMSystem.Core.ProjectionModels.Specialization;

namespace CRMSystem.Business.Abstractions;

public interface ISpecializationService
{
    Task<int> CreateSpecialization(SpecializationCreateModel createModel, CancellationToken ct);
    Task<int> DeleteSpecialization(int id, CancellationToken ct);
    Task<List<SpecializationItem>> GetSpecializations(CancellationToken ct);
    Task<int> UpdateSpecialization(int id, string? name, CancellationToken ct);
}