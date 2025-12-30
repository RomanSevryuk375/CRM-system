using CRM_system_backend.Contracts;
using CRM_system_backend.Contracts.Expense;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Expense;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase 
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseItem>>> GetPagedExpense([FromQuery] ExpenseFilter filter)
    {
        var dto = await _expenseService.GetPagedExpenses(filter);
        var count = await _expenseService.GetCountExpenses(filter);

        var response = dto
            .Select(e => new ExpenseResponse(
                e.id,
                e.date,
                e.category,
                e.tax,
                e.taxId,
                e.partSetId,
                e.expenseType,
                e.expenceTypeId,
                e.sum));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> CreateExpense([FromBody] ExpenseRequest request)
    {
        var (expense, errors) = Expense.Create(
            0,
            request.date,
            request.category,
            request.taxId,
            request.partSetId,
            request.expenseTypeId,
            request.sum);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _expenseService.CreateExpenses(expense!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> UpdateExpense(int id, [FromBody] ExpenseUpdateRequest request)
    {
        var model = new ExpenseUpdateModel(
            request.date,
            request.category,
            request.expenseTypeId,
            request.sum);

        var Id = await _expenseService.UpdateExpense(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> DeleteExpense(long id)
    {
        var result = await _expenseService.DeleteExpense(id);

        return Ok(result);
    }
}
