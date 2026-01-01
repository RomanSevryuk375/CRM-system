using CRMSystem.Core.DTOs.Shift;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IShiftService
{
    Task<int> CreateShift(Shift shift);
    Task<int> DeleteShift(int id);
    Task<List<ShiftItem>> GetShifts();
    Task<int> UpdateShift(int id, ShiftUpdateModel model);
}