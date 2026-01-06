using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IPartCategoryService
{
    Task<int> CreatePartCategory(PartCategory partCategory);
    Task<int> DeletePartCategory(int id);
    Task<List<PartCategoryItem>> GetPartCategories();
    Task<int> UpdatePartCategory(int id, PartCategoryUpdateModel model);
}