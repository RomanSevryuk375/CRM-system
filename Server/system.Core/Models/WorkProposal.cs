using CRMSystem.Core.Enums;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class WorkProposal
{
    public WorkProposal(long id, long orderId, long jobId, int wokerId, ProposalStatusEnum statusId, DateTime date)
    {
        Id = id;
        OrderId = orderId;
        JobId = jobId;
        WorkerId = wokerId;
        StatusId = statusId;
        Date = date;
    }
    public long Id { get; }
    public long OrderId { get; }
    public long JobId { get; }
    public int WorkerId { get; }
    public ProposalStatusEnum StatusId { get; }
    public DateTime Date { get; }

    public static (WorkProposal? workPropossal, List<string> errors) Create(long id, long orderId, long jobId, int wokerId, ProposalStatusEnum statusId, DateTime date)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var orderIdError = DomainValidator.ValidateId(orderId, "orderId");
        if (!string.IsNullOrEmpty(orderIdError)) errors.Add(orderIdError);

        var jobIdError = DomainValidator.ValidateId(jobId, "jobId");
        if (!string.IsNullOrEmpty(jobIdError)) errors.Add(jobIdError);

        var workerIdError = DomainValidator.ValidateId(wokerId, "workerId");
        if (!string.IsNullOrEmpty(workerIdError)) errors.Add(workerIdError);

        var statusError = DomainValidator.ValidateId(statusId, "status");
        if (!string.IsNullOrEmpty(statusError)) errors.Add(statusError);

        var dateError = DomainValidator.ValidateDate(date, "date");
        if (!string.IsNullOrEmpty(dateError)) errors.Add(dateError);

        if (errors.Any())
            return (null, errors);

        var workPropossal = new WorkProposal(id, orderId, jobId, wokerId, statusId, date);

        return (workPropossal, new List<string>());
    }
}
