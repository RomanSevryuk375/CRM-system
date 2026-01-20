using CRMSystem.Core.Validation;
using Shared.Enums;

namespace CRMSystem.Core.Models;

public class WorkInOrder
{
    private WorkInOrder(long id, long orderId, long jobId, int workerId, WorkStatusEnum statusId, decimal timeSpent)
    {
        Id = id;
        OrderId = orderId;
        JobId = jobId;
        WorkerId = workerId;
        StatusId = statusId;
        TimeSpent = timeSpent;
    }

    public long Id { get; }
    public long OrderId { get; }
    public long JobId { get; }
    public int WorkerId { get; }
    public decimal TimeSpent { get; }
    public WorkStatusEnum StatusId { get; }
    
    public static (WorkInOrder? workInOrder, List<string> errors) Create(long id, long orderId, long jobId, int workerId, WorkStatusEnum statusId, decimal timeSpent)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var orderIdError = DomainValidator.ValidateId(orderId, "orderId");
        if (!string.IsNullOrEmpty(orderIdError)) errors.Add(orderIdError);

        var jobIdError = DomainValidator.ValidateId(jobId, "jobId");
        if (!string.IsNullOrEmpty(jobIdError)) errors.Add(jobIdError);

        var workerIdError = DomainValidator.ValidateId(workerId, "workerId");
        if (!string.IsNullOrEmpty(workerIdError)) errors.Add(workerIdError);

        var stutusErrorId = DomainValidator.ValidateId(statusId, "status");
        if (!string.IsNullOrEmpty(stutusErrorId)) errors.Add(stutusErrorId);

        var timeSpentError = DomainValidator.ValidateMoney(timeSpent, "time");
        if (!string.IsNullOrEmpty(timeSpentError)) errors.Add(timeSpentError);

        if (errors.Any())
            return (null, errors);

        var workInOrder = new WorkInOrder(id,  orderId, jobId, workerId, statusId, timeSpent);

        return (workInOrder, new List<string>());
    }
}
