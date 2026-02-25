using CRMSystem.Core.ProjectionModels.Shift;

namespace CRMSystem.Business.Abstractions;

public interface IShiftService
{
    Task<int> CreateShift(ShiftCreateModel createModel, CancellationToken ct);
    Task<int> DeleteShift(int id, CancellationToken ct);
    Task<List<ShiftItem>> GetShifts(CancellationToken ct);
    Task<ShiftItem> GetShiftById(int id, CancellationToken ct);
    Task<int> UpdateShift(int id, ShiftUpdateModel model, CancellationToken ct);
}