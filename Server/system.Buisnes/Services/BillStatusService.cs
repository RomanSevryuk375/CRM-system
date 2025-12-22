using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class BillStatusService : IBillStatusService
{
    private readonly IBillStatusRepository _billStatusRepository;
    private readonly ILogger<BillStatusService> _logger;

    public BillStatusService(
        IBillStatusRepository billStatusRepository,
        ILogger<BillStatusService> logger)
    {
        _billStatusRepository = billStatusRepository;
        _logger = logger;
    }

    public async Task<List<BillStatusItem>> GetAllBillStatuses()
    {
        _logger.LogInformation("Getting bill statuses start");

        var bullStatus = await _billStatusRepository.Get();

        _logger.LogInformation("Getting bill statuses success");

        return bullStatus;
    }
}
