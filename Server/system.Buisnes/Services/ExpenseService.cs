using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Expense;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRespository;
    private readonly IExpenseTypeRepository _expenseTypeRepository;
    private readonly IPartSetRepository _partSetRepository;
    private readonly ITaxRepository _taxRepository;
    private readonly ILogger<ExpenseService> _logger;

    public ExpenseService(
        IExpenseRepository expenseRepository,
        IExpenseTypeRepository expenseTypeRepository,
        IPartSetRepository partSetRepository,
        ITaxRepository taxRepository,
        ILogger<ExpenseService> logger)
    {
        _expenseRespository = expenseRepository;
        _expenseTypeRepository = expenseTypeRepository;
        _partSetRepository = partSetRepository;
        _taxRepository = taxRepository;
        _logger = logger;
    }

    public async Task<List<ExpenseItem>> GetPagedExpenses(ExpenseFilter filter)
    {
        _logger.LogInformation("Getting expenses start");

        var client = await _expenseRespository.GetPaged(filter);

        _logger.LogInformation("Getting expenses success");

        return client;
    }

    public async Task<int> GetCountExpenses(ExpenseFilter filter)
    {
        _logger.LogInformation("Getting count expenses start");

        var client = await _expenseRespository.GetCount(filter);

        _logger.LogInformation("Getting count expenses success");

        return client;
    }

    public async Task<long> CreateExpenses(Expense expense)
    {
        _logger.LogInformation("Creating expenses start");

        if (!await _expenseTypeRepository.Exists((int)expense.ExpenseTypeId))
        {
            _logger.LogError("Expense{ExpenseTypeId} not found", expense.ExpenseTypeId);
            throw new NotFoundException($"Expense{(int)expense.ExpenseTypeId} not found");
        }

        if (expense.TaxId.HasValue
                && !await _taxRepository.Exists((int)expense.TaxId.Value))
        {
            _logger.LogError("Tax{TaxId} not found", (int)expense.TaxId);
            throw new NotFoundException($"Tax{(int)expense.TaxId} not found");
        }

        if (expense.PartSetId.HasValue
                && !await _partSetRepository.Exists(expense.PartSetId.Value))
        {
            _logger.LogError("PartSet{PartSetId} not found", (int)expense.PartSetId.Value);
            throw new NotFoundException($"PartSet{(int)expense.PartSetId.Value} not found");
        }

        _logger.LogInformation("Creating expenses success");

        var Id = await _expenseRespository.Create(expense);

        return Id;
    }

    public async Task<long> UpdateExpense(long id, ExpenseUpdateModel model)
    {
        _logger.LogInformation("Updating expenses start");

        if (model.ExpenseTypeId.HasValue
                && !await _expenseTypeRepository.Exists((int)model.ExpenseTypeId.Value))
        {
            _logger.LogError("Expense{ExpenseTypeId} not found", model.ExpenseTypeId.Value);
            throw new NotFoundException($"Expense{(int)model.ExpenseTypeId.Value} not found");
        }

        var Id = await _expenseRespository.Update(id, model);

        _logger.LogInformation("Updating expenses success");

        return Id;
    }

    public async Task<long> DeleteExpense(long id)
    {
        _logger.LogInformation("Deleting expenses start");

        var Id = await _expenseRespository.Delete(id);

        _logger.LogInformation("Deleting expenses success");

        return Id;
    }
}
