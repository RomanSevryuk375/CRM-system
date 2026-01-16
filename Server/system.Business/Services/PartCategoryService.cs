// Ignore Spelling: repo

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class PartCategoryService : IPartCategoryService
{
    private readonly IPartCategoryRepository _repo;
    private readonly ILogger<PartCategoryService> _logger;

    public PartCategoryService(
        IPartCategoryRepository repo,
        ILogger<PartCategoryService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<List<PartCategoryItem>> GetPartCategories(CancellationToken ct)
    {
        _logger.LogInformation("Getting part category start");

        var categories = await _repo.Get(ct);

        _logger.LogInformation("Getting part category success");

        return categories;
    }

    public async Task<int> CreatePartCategory(PartCategory partCategory, CancellationToken ct)
    {
        _logger.LogInformation("Creating part category start");

        if (await _repo.NameExists(partCategory.Name, ct))
        {
            _logger.LogError("Category with this this name{Name} is exist", partCategory.Name);
            throw new ConflictException($"Category with this this name{partCategory.Name} is exist");
        }

        var Id = await _repo.Create(partCategory, ct);

        _logger.LogInformation("Creating part category success");

        return Id;
    }

    public async Task<int> UpdatePartCategory(int id, PartCategoryUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating part category start");

        if (!string.IsNullOrEmpty(model.Name) && await _repo.NameExists(model.Name, ct))
        {
            _logger.LogError("Category with this this name{Name} is exist", model.Name);
            throw new ConflictException($"Category with this this name{model.Name} is exist");
        }

        var Id = await _repo.Update(id, model, ct);

        _logger.LogInformation("Updating part category success");

        return Id;
    }

    public async Task<int> DeletePartCategory(int id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting part category start");

        var Id = await _repo.Delete(id, ct);

        _logger.LogInformation("Deleting part category success");

        return Id;
    }
}
