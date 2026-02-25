using AutoMapper;
using CRMSystem.Business.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/expense-types")]
public class ExpenseTypeController(
    IExpenseTypeService expenseTypeService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<ExpenseTypeResponse>>> GetExpenseType(
        CancellationToken ct)
    {
        var dto = await expenseTypeService.GetExpenseType(ct);
        var response = mapper.Map<List<ExpenseTypeResponse>>(dto);

        return Ok(response);
    }
}
