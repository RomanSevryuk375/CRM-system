namespace CRMSystem.Buisnes.DTOs;

public record ExpensesWitInfoDto
(
    int Id, 
    DateTime Date, 
    string Category, 
    string? TaxName, 
    string? UsedPartInfo, 
    string ExpenseType, 
    decimal Sum
);
