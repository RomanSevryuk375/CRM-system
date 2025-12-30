using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseTypeController : ControllerBase
{
    private readonly IExpenseTypeService _expenseTypeService;

    public ExpenseTypeController(IExpenseTypeService expenseTypeService)
    {
        _expenseTypeService = expenseTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseTypeItem>>> GetExpenseType()
    {
        var dto = await _expenseTypeService.GetExpenseType();

        var response = dto.Select(e => new ExpenseTypeResponse(
            e.Id,
            e.Name));

        return Ok(response);
    }
}
