using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ISpecializationRepository
{
    Task<int> Create(Specialization specialization, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<List<SpecializationItem>> Get(CancellationToken ct);
    Task<int> Update(int id, string? name, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
    Task<bool> ExistsByName(string name, CancellationToken ct);
}