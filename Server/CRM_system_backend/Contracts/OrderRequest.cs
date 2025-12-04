namespace CRM_system_backend.Contracts;

public record OrderRequest
(
    int? StatusId,
    int? CarId,
    DateTime? Date,
    string? Priority
);
