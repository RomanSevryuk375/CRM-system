using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Expense;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/expenses")]
public class ExpenseController(
    IExpenseService expenseService,
    IMapper mapper) : ControllerBase 
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<ExpenseItem>>> GetPagedExpense(
        [FromQuery] ExpenseFilter filter, CancellationToken ct)
    {
        var dto = await expenseService.GetPagedExpenses(filter, ct);
        var count = await expenseService.GetCountExpenses(filter, ct);

        var response = mapper.Map<List<ExpenseResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateExpense(
        [FromBody] ExpenseRequest request, CancellationToken ct)
    {
        var (expense, errors) = Expense.Create(
            0,
            request.Date,
            request.Category,
            request.TaxId,
            request.PartSetId,
            request.ExpenseTypeId,
            request.Sum);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await expenseService.CreateExpenses(expense!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateExpense(
        int id, [FromBody] ExpenseUpdateRequest request, CancellationToken ct)
    {
        var model = new ExpenseUpdateModel(
            request.Date,
            request.Category,
            request.ExpenseTypeId,
            request.Sum);

        await expenseService.UpdateExpense(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteExpense(
        long id, CancellationToken ct)
    {
        await expenseService.DeleteExpense(id, ct);

        return NoContent();
    }
}
