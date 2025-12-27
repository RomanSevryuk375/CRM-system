using CRMSystem.Core.DTOs.Shift;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IShiftRepository
{
    Task<int> Create(Shift shift);
    Task<int> Delete(int id);
    Task<List<ShiftItem>> Get();
    Task<int> Update(int id, ShiftUpdateModel model);
    Task<bool> Exists(int id);
    Task<bool> HasOverLap(TimeOnly start, TimeOnly end);
}