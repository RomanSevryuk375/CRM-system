namespace CRM_system_backend.Contracts.WorkPropossal;

public record WorkProposalResponse
(
    long id,
    long orderId,
    string job,
    long jobId,
    string woker,
    int workerId,
    string status,
    int statusId,
    DateTime date
);
