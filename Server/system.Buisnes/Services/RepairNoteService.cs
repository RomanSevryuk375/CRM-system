using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class RepairNoteService : IRepairNoteService
{
    private readonly IRepairNoteRepositry _repairNoteRepositry;
    private readonly ICarRepository _carRepository;

    public RepairNoteService(IRepairNoteRepositry repairNoteRepositry, ICarRepository carRepository)
    {
        _repairNoteRepositry = repairNoteRepositry;
        _carRepository = carRepository;
    }

    public async Task<List<RepairNote>> GetRepairNote()
    {
        return await _repairNoteRepositry.Get();
    }

    public async Task<List<RepairNoteWithInfoDto>> GetRepairNoteWithInfo()
    {
        var repairHistories = await _repairNoteRepositry.Get();
        var cars = await _carRepository.Get();

        var response = (from r in repairHistories
                        join c in cars on r.CarId equals c.Id
                        select new RepairNoteWithInfoDto(
                            r.Id,
                            r.OrderId,
                            $"{c.Brand} {c.Model} ({c.StateNumber})",
                            r.WorkDate,
                            r.ServiceSum))
                            .ToList();

        return response;
    }
}
