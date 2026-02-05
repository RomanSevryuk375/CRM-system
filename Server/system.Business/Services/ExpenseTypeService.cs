using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class ExpenseTypeService(
    IExpenseTypeRepository expenseTypeRepository,
    ILogger<ExpenseTypeService> logger) : IExpenseTypeService
{
    public async Task<List<ExpenseTypeItem>> GetExpenseType(CancellationToken ct)
    {
        logger.LogInformation("Getting absenceType start");

        var expenseTypes = await expenseTypeRepository.Get(ct);

        logger.LogInformation("Getting absenceType success");

        return expenseTypes;
    }
}
