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
    public async Task<ExpenseItem> GetExpenseById(long id, CancellationToken ct)
    {
        return await expenseRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Expense {id} not found");
    }
    
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

    public async Task<long> CreateExpenses(ExpenseCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating expenses start");

        if (!await expenseTypeRepository.Exists((int)createModel.ExpenseTypeId, ct))
        {
            logger.LogError("Expense{ExpenseTypeId} not found", createModel.ExpenseTypeId);
            throw new NotFoundException($"Expense{(int)createModel.ExpenseTypeId} not found");
        }

        if (createModel.TaxId.HasValue
                && !await taxRepository.Exists(createModel.TaxId.Value, ct))
        {
            logger.LogError("Tax{TaxId} not found", (int)createModel.TaxId);
            throw new NotFoundException($"Tax{(int)createModel.TaxId} not found");
        }

        if (createModel.PartSetId.HasValue
                && !await partSetRepository.Exists(createModel.PartSetId.Value, ct))
        {
            logger.LogError("PartSet{PartSetId} not found", (int)createModel.PartSetId.Value);
            throw new NotFoundException($"PartSet{(int)createModel.PartSetId.Value} not found");
        }
        
        var (expense, errors) = Expense.Create(
            0,
            createModel.Date,
            createModel.Category,
            createModel.TaxId,
            createModel.PartSetId,
            createModel.ExpenseTypeId,
            createModel.Sum);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        logger.LogInformation("Creating expenses success");

        var id = await expenseRepository.Create(expense!, ct);

        return id;
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

        var expenseId = await expenseRepository.Update(id, model, ct);

        logger.LogInformation("Updating expenses success");

        return expenseId;
    }

    public async Task<long> DeleteExpense(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting expenses start");

        var expenseId = await expenseRepository.Delete(id, ct);

        logger.LogInformation("Deleting expenses success");

        return expenseId;
    }
}
