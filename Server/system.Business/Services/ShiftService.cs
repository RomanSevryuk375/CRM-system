using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Shift;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class ShiftService : IShiftService
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

    public async Task<List<ShiftItem>> GetShifts(CancellationToken ct)
    {
        _logger.LogInformation("Getting shift start");

        var shifts = await _shiftRepository.Get(ct);

        _logger.LogInformation("Getting shift success");

        return shifts;
    }

    public async Task<int> CreateShift(Shift shift, CancellationToken ct)
    {
        _logger.LogInformation("Creating shift start");

        if (await _shiftRepository.HasOverLap(shift.StartAt, shift.EndAt, ct))
        {
            _logger.LogInformation("Has date overlaps");
            throw new ConflictException($"Has date overlaps");
        }

        var Id = await _shiftRepository.Create(shift, ct);

        _logger.LogInformation("Creating shift success");

        return Id;
    }

    public async Task<int> UpdateShift(int id, ShiftUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating shift start");

        var Id = await _shiftRepository.Update(id, model, ct);

        _logger.LogInformation("Updating shift success");

        return Id;
    }

    public async Task<int> DeleteShift(int id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting shift start");

        var Id = await _shiftRepository.Delete(id, ct);

        _logger.LogInformation("Deleting shift success");

        return Id;
    }
}
