using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.Expense;

public record ExpenseUpdateModel
(
    DateTime? Date,
    string? Category,
    ExpenseTypeEnum? ExpenseTypeId,
    decimal? Sum
);
