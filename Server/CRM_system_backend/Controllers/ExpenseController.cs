using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpenseController : ControllerBase 
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<List<Expense>>> GetExpense()
    {
        var expenses = await _expenseService.GetExpenses();

        var response = expenses
            .Select(e => new ExpenseResponse(
                e.Id,
                e.Date,
                e.Category,
                e.TaxId,
                e.UsedPartId,
                e.ExpenseType,
                e.Sum))
            .ToList();

        return Ok(response);
    }

    [HttpGet("with-Info")]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<List<ExpensesWitInfoDto>>> GetExpenseWithInfo()
    {
        var dtos = await _expenseService.GetExpensesWithInfo();

        var response = dtos
            .Select(d => new ExpensesWitInfoDto(
                d.Id,
                d.Date,
                d.Category,
                d.TaxName,
                d.UsedPartInfo,
                d.ExpenseType,
                d.Sum))
            .ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> CreateExpense([FromBody] ExpenseRequest request)
    {
        var (expense, error) = Expense.Create(
            0,
            request.Date,
            request.Category,
            request.TaxId,
            request.UsedPartId,
            request.ExpenseType,
            request.Sum);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var expenseId = await _expenseService.CreateExpense(expense);

        return Ok(expenseId);
    }

    [HttpPut("${id}")]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> UpdateExpense([FromBody] ExpenseRequest request, int id)
    {
        var result = await _expenseService.UpdateExpense(
            id,
            request.Date,
            request.Category,
            request.TaxId,
            request.UsedPartId,
            request.ExpenseType,
            request.Sum);

        return Ok(result);
    }

    [HttpDelete("${id}")]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> DeleteExpense(int id)
    {
        var result = await _expenseService.DeleteExpense(id);

        return Ok(result);
    }
}
