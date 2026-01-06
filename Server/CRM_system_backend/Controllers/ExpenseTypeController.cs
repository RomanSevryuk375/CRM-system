using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseTypeController : ControllerBase
{
    private readonly IExpenseTypeService _expenseTypeService;
    private readonly IMapper _mapper;

    public ExpenseTypeController(
        IExpenseTypeService expenseTypeService,
        IMapper mapper)
    {
        _expenseTypeService = expenseTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseTypeItem>>> GetExpenseType()
    {
        var dto = await _expenseTypeService.GetExpenseType();

        var response = _mapper.Map<List<ExpenseTypeResponse>>(dto);

        return Ok(response);
    }
}
