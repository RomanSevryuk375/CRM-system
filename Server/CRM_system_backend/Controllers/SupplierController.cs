using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Controller]
[Route("[controller]")]
public class SupplierController : Controller
{
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]

    public async Task<ActionResult<List<Supplier>>> GetSuppliers()
    {
        var suppliers = await _supplierService.GetSupplier();

        var response = suppliers
            .Select(s => new SupplierResponse(s.Id, s.Name, s.Contacts))
            .ToList();

        return Ok(response);
    }

    [HttpPost]

    public async Task<ActionResult<int>> CreateSupplier ([FromBody] SupplierRequest supplierRequest)
    {
        var (supplier, error) = Supplier.Create
            (
            0,
            supplierRequest.Name,
            supplierRequest.Contacts
            );

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var result = await _supplierService.CreateSupplier(supplier);

        return Ok(result);
    }

    [HttpPut]

    public async Task<ActionResult<int>> UpdateSupplier([FromBody] SupplierRequest supplierRequest, int id)
    {
        var result = await _supplierService.UpdateSupplier(id, supplierRequest.Name, supplierRequest.Contacts);

        return Ok(result);
    }

    [HttpDelete]

    public async Task<ActionResult<int>> DeleteSupplier(int id)
    {
        var result = await _supplierService.DeleteSupplier(id);

        return Ok(result);
    }
}
