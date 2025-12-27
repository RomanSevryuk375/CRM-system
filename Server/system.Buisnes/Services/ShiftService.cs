using CRMSystem.Core.DTOs.Shift;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class ShiftService
{
    private readonly IShiftRepository _shiftRepository;
    private readonly ILogger<ShiftService> _logger;

    public ShiftService(
        IShiftRepository shiftRepository,
        ILogger<ShiftService> logger)
    {
        _shiftRepository = shiftRepository;
        _logger = logger;
    }

    public async Task<List<ShiftItem>> GetShifts()
    {
        _logger.LogInformation("Getting shift start");

        var shifts = await _shiftRepository.Get();

        _logger.LogInformation("Getting shift success");

        return shifts;
    }

    public async Task<int> CreateShift(Shift shift)
    {
        _logger.LogInformation("Creating shift start");

        if (await _shiftRepository.HasOverLap(shift.StartAt, shift.EndAt))
        {
            _logger.LogInformation("Has date overlaps");
            throw new ConflictException($"Has date overlaps");
        }

        var Id = await _shiftRepository.Create(shift);

        _logger.LogInformation("Creating shift success");

        return Id;
    }

    public async Task<int> UpdateShift(int id, ShiftUpdateModel model)
    {
        _logger.LogInformation("Updating shift start");

        var Id = await _shiftRepository.Update(id, model);

        _logger.LogInformation("Updating shift success");

        return Id;
    }

    public async Task<int> DeleteShift(int id)
    {
        _logger.LogInformation("Deleting shift start");

        var Id = await _shiftRepository.Delete(id);

        _logger.LogInformation("Deleting shift success");

        return Id;
    }
}
