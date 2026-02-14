using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Expense;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class ExpenseService(
    IExpenseRepository expenseRepository,
    IExpenseTypeRepository expenseTypeRepository,
    IPartSetRepository partSetRepository,
    ITaxRepository taxRepository,
    ILogger<ExpenseService> logger) : IExpenseService
{
    public async Task<List<ExpenseItem>> GetPagedExpenses(ExpenseFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting expenses start");

        var client = await expenseRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting expenses success");

        return client;
    }

    public async Task<int> GetCountExpenses(ExpenseFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count expenses start");

        var client = await expenseRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count expenses success");

        return client;
    }

    public async Task<long> CreateExpenses(Expense expense, CancellationToken ct)
    {
        logger.LogInformation("Creating expenses start");

        if (!await expenseTypeRepository.Exists((int)expense.ExpenseTypeId, ct))
        {
            logger.LogError("Expense{ExpenseTypeId} not found", expense.ExpenseTypeId);
            throw new NotFoundException($"Expense{(int)expense.ExpenseTypeId} not found");
        }

        if (expense.TaxId.HasValue
                && !await taxRepository.Exists(expense.TaxId.Value, ct))
        {
            logger.LogError("Tax{TaxId} not found", (int)expense.TaxId);
            throw new NotFoundException($"Tax{(int)expense.TaxId} not found");
        }

        if (expense.PartSetId.HasValue
                && !await partSetRepository.Exists(expense.PartSetId.Value, ct))
        {
            logger.LogError("PartSet{PartSetId} not found", (int)expense.PartSetId.Value);
            throw new NotFoundException($"PartSet{(int)expense.PartSetId.Value} not found");
        }

        logger.LogInformation("Creating expenses success");

        var Id = await expenseRepository.Create(expense, ct);

        return Id;
    }

    public async Task<long> UpdateExpense(long id, ExpenseUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating expenses start");

        if (model.ExpenseTypeId.HasValue
                && !await expenseTypeRepository.Exists((int)model.ExpenseTypeId.Value, ct))
        {
            logger.LogError("Expense{ExpenseTypeId} not found", model.ExpenseTypeId.Value);
            throw new NotFoundException($"Expense{(int)model.ExpenseTypeId.Value} not found");
        }

        var Id = await expenseRepository.Update(id, model, ct);

        logger.LogInformation("Updating expenses success");

        return Id;
    }

    public async Task<long> DeleteExpense(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting expenses start");

        var Id = await expenseRepository.Delete(id, ct);

        logger.LogInformation("Deleting expenses success");

        return Id;
    }
}
