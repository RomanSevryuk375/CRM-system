namespace CRM_system_backend.Contracts.PartCategory;

public record PartCategoryResponse
(
    int id,
    string name,
    string? description
);
