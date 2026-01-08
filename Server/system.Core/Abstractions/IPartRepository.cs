using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IPartRepository
{
    Task<long> Create(Part part, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(PartFilter filter, CancellationToken ct);
    Task<List<PartItem>> GetPaged(PartFilter filter, CancellationToken ct);
    Task<long> Update(long id, PartUpdateModel model, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}