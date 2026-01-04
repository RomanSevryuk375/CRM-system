using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<RoleItem>>> GetRoles()
    {
        var dto = await _roleService.GetRoles();

        var response = _mapper.Map<List<RoleResponse>>(dto);

        return Ok(response);
    }
}
