using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Shift;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class ShiftService(
    IShiftRepository shiftRepository,
    ILogger<ShiftService> logger) : IShiftService
{
    public async Task<List<ShiftItem>> GetShifts(CancellationToken ct)
    {
        logger.LogInformation("Getting shift start");

        var shifts = await shiftRepository.Get(ct);

        logger.LogInformation("Getting shift success");

        return shifts;
    }

    public async Task<int> CreateShift(Shift shift, CancellationToken ct)
    {
        logger.LogInformation("Creating shift start");

        if (await shiftRepository.HasOverLap(shift.StartAt, shift.EndAt, ct))
        {
            logger.LogInformation("Has date overlaps");
            throw new ConflictException("Has date overlaps");
        }

        var Id = await shiftRepository.Create(shift, ct);

        logger.LogInformation("Creating shift success");

        return Id;
    }

    public async Task<int> UpdateShift(int id, ShiftUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating shift start");

        var Id = await shiftRepository.Update(id, model, ct);

        logger.LogInformation("Updating shift success");

        return Id;
    }

    public async Task<int> DeleteShift(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting shift start");

        var Id = await shiftRepository.Delete(id, ct);

        logger.LogInformation("Deleting shift success");

        return Id;
    }
}
