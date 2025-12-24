using CRMSystem.Core.DTOs.PartCategory;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IPartCategoryRepository
{
    Task<int> Create(PartCategory category);
    Task<int> Delete(int id);
    Task<List<PartCategoryItem>> Get();
    Task<int> Update(int id, PartCategoryUpdateModel model);
    Task<bool> Exists(int id);
    Task<bool> NameExists(string name);
}