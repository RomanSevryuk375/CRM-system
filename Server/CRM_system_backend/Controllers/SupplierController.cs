using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Supplier;

namespace CRM_system_backend.Controllers;

[Controller]
[Route("api/v1/suppliers")]
public class SupplierController(
    ISupplierService supplierService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<SupplierItem>>> GetSuppliers(CancellationToken ct)
    {
        var dto = await supplierService.GetSuppliers(ct);

        var response = mapper.Map<List<SupplierResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateSupplier(
        [FromBody] SupplierRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<SupplierCreateModel>(request);

        await supplierService.CreateSupplier(createModel, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateSupplier(
        int id, [FromBody] SupplierUpdateRequest request, CancellationToken ct)
    {
        var model = new SupplierUpdateModel(
            request.Name,
            request.Contacts);

        await supplierService.UpdateSupplier(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteSupplier(
        int id, CancellationToken ct)
    {
        await supplierService.DeleteSupplier(id, ct);

        return NoContent();
    }
}
