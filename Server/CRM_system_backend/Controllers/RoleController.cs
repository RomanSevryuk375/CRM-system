using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/v1/roles")]
[ApiController]
public class RoleController(
    IRoleService roleService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<RoleItem>>> GetRoles(CancellationToken ct)
    {
        var dto = await roleService.GetRoles(ct);

        var response = mapper.Map<List<RoleResponse>>(dto);

        return Ok(response);
    }
}
