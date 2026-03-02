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
    public async Task<ActionResult<List<SupplierResponse>>> GetSuppliers(CancellationToken ct)
    {
        var dto = await supplierService.GetSuppliers(ct);
        var response = mapper.Map<List<SupplierResponse>>(dto);

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<SupplierResponse>> GetSupplierById(int id, CancellationToken ct)
    {
        var dto = await supplierService.GetSupplierById(id, ct);
        var response = mapper.Map<SupplierResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateSupplier(
        [FromBody] SupplierRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<SupplierCreateModel>(request);
        var id = await supplierService.CreateSupplier(createModel, ct);
        
        var createdDto = await supplierService.GetSupplierById(id, ct);
        var response = mapper.Map<SupplierResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetSupplierById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateSupplier(
        int id, [FromBody] SupplierUpdateRequest request, CancellationToken ct)
    {
        var model = new SupplierUpdateModel(request.Name, request.Contacts);
        await supplierService.UpdateSupplier(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteSupplier(
        int id, CancellationToken ct)
    {
        await supplierService.DeleteSupplier(id, ct);

        return NoContent();
    }
}
