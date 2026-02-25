using AutoMapper;
using CRMSystem.Business.Abstractions;
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
    public async Task<ActionResult<List<ExpenseResponse>>> GetPagedExpense(
        [FromQuery] ExpenseFilter filter, CancellationToken ct)
    {
        var dto = await expenseService.GetPagedExpenses(filter, ct);
        var count = await expenseService.GetCountExpenses(filter, ct);

        var response = mapper.Map<List<ExpenseResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<ExpenseResponse>> GetExpenseById(
        int id, CancellationToken ct)
    {
        var dto = await expenseService.GetExpenseById(id, ct);
        var response = mapper.Map<ExpenseResponse>(dto);

        return Ok(response);
    }
    
    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateExpense(
        [FromBody] ExpenseRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<ExpenseCreateModel>(request);
        var id = await expenseService.CreateExpenses(createModel!, ct);
        
        var createdDto = await expenseService.GetExpenseById(id, ct);
        var response = mapper.Map<ExpenseResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetExpenseById), 
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateExpense(
        int id, [FromBody] ExpenseUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<ExpenseUpdateModel>(request);
        await expenseService.UpdateExpense(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteExpense(
        int id, CancellationToken ct)
    {
        await expenseService.DeleteExpense(id, ct);

        return NoContent();
    }
}
