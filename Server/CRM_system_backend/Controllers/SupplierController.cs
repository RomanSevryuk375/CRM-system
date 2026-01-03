using CRM_system_backend.Contracts;
using CRM_system_backend.Contracts.Supplier;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Supplier;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Controller]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SupplierItem>>> GetSuppliers()
    {
        var dto = await _supplierService.GetSuppliers();

        var response = dto.Select(s => new SupplierResponse(
            s.Id, 
            s.Name, 
            s.Contacts));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateSupplier ([FromBody] SupplierRequest request)
    {
        var (supplier, errors) = Supplier.Create(
            0,
            request.Name,
            request.Contacts);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _supplierService.CreateSupplier(supplier!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateSupplier(int id, [FromBody] SupplierUpdateRequest request)
    {
        var model = new SupplierUpdateModel(
            request.Name,
            request.Contacts);

        var Id = await _supplierService.UpdateSupplier(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteSupplier(int id)
    {
        var result = await _supplierService.DeleteSupplier(id);

        return Ok(result);
    }
}
