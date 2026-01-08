using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IPositionRepository
{
    Task<long> Create(Position position, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(PositionFilter filter, CancellationToken ct);
    Task<List<PositionItem>> GetPaged(PositionFilter filter, CancellationToken ct);
    Task<long> Update(long id, PositionUpdateModel model, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}