using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public RoleController(
        IRoleService roleService,
        IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoleItem>>> GetRoles(CancellationToken ct)
    {
        var dto = await _roleService.GetRoles(ct);

        var response = _mapper.Map<List<RoleResponse>>(dto);

        return Ok(response);
    }
}
