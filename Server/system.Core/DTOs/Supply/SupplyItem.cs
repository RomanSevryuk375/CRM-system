namespace CRMSystem.Core.DTOs.Supply;

public record SupplyItem
(
    long id, 
    string supplier, 
    DateOnly date
);
