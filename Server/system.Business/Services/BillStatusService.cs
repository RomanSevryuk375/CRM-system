using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class BillStatusService(
    IBillStatusRepository billStatusRepository,
    ILogger<BillStatusService> logger) : IBillStatusService
{
    public async Task<List<BillStatusItem>> GetAllBillStatuses(CancellationToken ct)
    {
        logger.LogInformation("Getting bill statuses start");

        var bullStatus = await billStatusRepository.Get(ct);

        logger.LogInformation("Getting bill statuses success");

        return bullStatus;
    }
}
