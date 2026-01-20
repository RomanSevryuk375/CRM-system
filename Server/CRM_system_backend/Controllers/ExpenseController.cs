using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Expense;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Expense;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase 
{
    private readonly IExpenseService _expenseService;
    private readonly IMapper _mapper;

    public ExpenseController(
        IExpenseService expenseService,
        IMapper mapper)
    {
        _expenseService = expenseService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseItem>>> GetPagedExpense([FromQuery] ExpenseFilter filter, CancellationToken ct)
    {
        var dto = await _expenseService.GetPagedExpenses(filter, ct);
        var count = await _expenseService.GetCountExpenses(filter, ct);

        var response = _mapper.Map<List<ExpenseResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateExpense([FromBody] ExpenseRequest request, CancellationToken ct)
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
            return BadRequest(errors);

        var Id = await _expenseService.CreateExpenses(expense!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateExpense(int id, [FromBody] ExpenseUpdateRequest request, CancellationToken ct)
    {
        var model = new ExpenseUpdateModel(
            request.Date,
            request.Category,
            request.ExpenseTypeId,
            request.Sum);

        var Id = await _expenseService.UpdateExpense(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteExpense(long id, CancellationToken ct)
    {
        var result = await _expenseService.DeleteExpense(id, ct);

        return Ok(result);
    }
}
