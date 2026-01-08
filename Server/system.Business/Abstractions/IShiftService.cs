using CRMSystem.Core.ProjectionModels.Shift;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IShiftService
{
    Task<int> CreateShift(Shift shift, CancellationToken ct);
    Task<int> DeleteShift(int id, CancellationToken ct);
    Task<List<ShiftItem>> GetShifts(CancellationToken ct);
    Task<int> UpdateShift(int id, ShiftUpdateModel model, CancellationToken ct);
}