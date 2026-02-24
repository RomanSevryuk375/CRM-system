using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.Expense;

public record ExpenseCreateModel
(
    DateTime Date, 
    string Category, 
    int? TaxId,
    long? PartSetId, 
    ExpenseTypeEnum ExpenseTypeId,
    decimal Sum);