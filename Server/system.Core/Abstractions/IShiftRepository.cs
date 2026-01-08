using CRMSystem.Core.ProjectionModels.Shift;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IShiftRepository
{
    Task<int> Create(Shift shift, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<List<ShiftItem>> Get(CancellationToken ct);
    Task<int> Update(int id, ShiftUpdateModel model, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
    Task<bool> HasOverLap(TimeOnly start, TimeOnly end, CancellationToken ct);
}