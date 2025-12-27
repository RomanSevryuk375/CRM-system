using CRMSystem.Core.DTOs.PartCategory;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IPartCategoryService
{
    Task<int> CreatePartCategory(PartCategory partCategory);
    Task<int> DeletePartCategory(int id);
    Task<List<PartCategoryItem>> GetPartCategories();
    Task<int> UpdatePartCategory(int id, PartCategoryUpdateModel model);
}