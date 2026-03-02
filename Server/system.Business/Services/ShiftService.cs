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
    public async Task<ShiftItem> GetShiftById(int id, CancellationToken ct)
    {
        return await shiftRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Shift {id} not found");
    }
    
    public async Task<List<ShiftItem>> GetShifts(CancellationToken ct)
    {
        logger.LogInformation("Getting shift start");

        var shifts = await shiftRepository.Get(ct);

        logger.LogInformation("Getting shift success");

        return shifts;
    }

    public async Task<int> CreateShift(ShiftCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating shift start");

        if (await shiftRepository.HasOverLap(createModel.StartAt, createModel.EndAt, ct))
        {
            logger.LogInformation("Has date overlaps");
            throw new ConflictException("Has date overlaps");
        }
        var (shift, errors) = Shift.Create(
            0,
            createModel.Name,
            createModel.StartAt,
            createModel.EndAt);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }
        var id = await shiftRepository.Create(shift!, ct);

        logger.LogInformation("Creating shift success");

        return id;
    }

    public async Task<int> UpdateShift(int id, ShiftUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating shift start");

        var shiftId = await shiftRepository.Update(id, model, ct);

        logger.LogInformation("Updating shift success");

        return shiftId;
    }

    public async Task<int> DeleteShift(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting shift start");

        var shiftId = await shiftRepository.Delete(id, ct);

        logger.LogInformation("Deleting shift success");

        return shiftId;
    }
}
