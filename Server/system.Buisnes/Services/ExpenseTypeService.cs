using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class ExpenseTypeService : IExpenseTypeService
{
    private readonly IExpenseTypeRepository _expenseTypeRepository;
    private readonly ILogger<ExpenseTypeService> _logger;

    public ExpenseTypeService(
        IExpenseTypeRepository expenseTypeRepository,
        ILogger<ExpenseTypeService> logger)
    {
        _expenseTypeRepository = expenseTypeRepository;
        _logger = logger;
    }

    public async Task<List<ExpenseTypeItem>> GetExpenseType()
    {
        _logger.LogInformation("Getting absenceType start");

        var expenseTypes = await _expenseTypeRepository.Get();

        _logger.LogInformation("Getting absenceType success");

        return expenseTypes;
    }
}
