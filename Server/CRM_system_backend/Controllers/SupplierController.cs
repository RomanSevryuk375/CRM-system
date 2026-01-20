using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Supplier;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Supplier;

namespace CRM_system_backend.Controllers;

[Controller]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _supplierService;
    private readonly IMapper _mapper;

    public SupplierController(
        ISupplierService supplierService,
        IMapper mapper)
    {
        _supplierService = supplierService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<SupplierItem>>> GetSuppliers(CancellationToken ct)
    {
        var dto = await _supplierService.GetSuppliers(ct);

        var response = _mapper.Map<List<SupplierResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateSupplier ([FromBody] SupplierRequest request, CancellationToken ct)
    {
        var (supplier, errors) = Supplier.Create(
            0,
            request.Name,
            request.Contacts);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _supplierService.CreateSupplier(supplier!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateSupplier(int id, [FromBody] SupplierUpdateRequest request, CancellationToken ct)
    {
        var model = new SupplierUpdateModel(
            request.Name,
            request.Contacts);

        var Id = await _supplierService.UpdateSupplier(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteSupplier(int id, CancellationToken ct)
    {
        var result = await _supplierService.DeleteSupplier(id, ct);

        return Ok(result);
    }
}
