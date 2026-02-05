using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Guarantee;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class GuaranteeService(
    IGuaranteeRepository guaranteeRepository,
    IOrderRepository orderRepository,
    ILogger<GuaranteeService> logger) : IGuaranteeService
{
    public async Task<List<GuaranteeItem>> GetPagedGuarantees(GuaranteeFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting guarantees start"); 

        var guarantee = await guaranteeRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting guarantees success"); 

        return guarantee;
    }

    public async Task<int> GetCountGuarantees(GuaranteeFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count guarantees start"); 

        var count = await guaranteeRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count guarantees success"); 

        return count;
    }

    public async Task<long> CreateGuarantee(Guarantee guarantee, CancellationToken ct)
    {
        logger.LogInformation("Creating guarantee for order {OrderId} start", guarantee.OrderId);

        if (!await orderRepository.Exists(guarantee.OrderId, ct))
        {
            logger.LogError("Order{OrderId} not found", guarantee.OrderId);
            throw new NotFoundException($"Order{guarantee.OrderId} not found");
        }

        var Id = await guaranteeRepository.Create(guarantee, ct);

        logger.LogInformation("Creating guarantee for order {OrderId} success with ID {GuaranteeId}", guarantee.OrderId, Id);

        return Id;
    }

    public async Task<long> UpdateGuarantee(long id, GuaranteeUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating guarantee {Id} start", id);

        var Id = await guaranteeRepository.Update(id, model, ct);

        logger.LogInformation("Updating guarantee {Id} success", id);

        return Id;
    }

    public async Task<long> DeleteGuarantee(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting guarantee {Id} start", id);

        var Id = await guaranteeRepository.Delete(id, ct);

        logger.LogInformation("Deleting guarantee {Id} success", id);

        return Id;
    }
}
