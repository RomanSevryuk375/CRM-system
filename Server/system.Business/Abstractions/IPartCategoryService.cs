using CRMSystem.Core.ProjectionModels.PartCategory;

namespace CRMSystem.Business.Abstractions;

public interface IPartCategoryService
{
    Task<int> CreatePartCategory(PartCategoryCreateModel createModel, CancellationToken ct);
    Task<int> DeletePartCategory(int id, CancellationToken ct);
    Task<List<PartCategoryItem>> GetPartCategories(CancellationToken ct);
    Task<PartCategoryItem> GetPartCategoryById(int id, CancellationToken ct);
    Task<int> UpdatePartCategory(int id, PartCategoryUpdateModel model, CancellationToken ct);
}