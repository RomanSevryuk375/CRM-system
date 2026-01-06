using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IPartCategoryRepository
{
    Task<int> Create(PartCategory category);
    Task<int> Delete(int id);
    Task<List<PartCategoryItem>> Get();
    Task<int> Update(int id, PartCategoryUpdateModel model);
    Task<bool> Exists(int id);
    Task<bool> NameExists(string name);
}