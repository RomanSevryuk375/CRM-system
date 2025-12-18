using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Tax;

public record TaxItem
(
    int id, 
    string name, 
    decimal rate, 
    string type
);
