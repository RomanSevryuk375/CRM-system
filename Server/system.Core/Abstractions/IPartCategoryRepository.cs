using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IPartCategoryRepository
{
    Task<int> Create(PartCategory category, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<List<PartCategoryItem>> Get(CancellationToken ct);
    Task<int> Update(int id, PartCategoryUpdateModel model, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
    Task<bool> NameExists(string name, CancellationToken ct);
}