namespace CRM_system_backend.Contracts.Supplier;

public record SupplierUpdateRequest
(
    string? name,
    string? contacts
);
