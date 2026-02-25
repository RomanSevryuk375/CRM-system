using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Car;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/cars")]
public class CarController(
    ICarService carService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<List<CarResponse>>> GetPagedCars(
        [FromQuery]CarFilter filter, CancellationToken ct)
    {
        var dto = await carService.GetPagedCars(filter, ct);
        var cout = await carService.GetCountCars(filter, ct);

        var response = mapper.Map<List<CarResponse>>(dto);

        Response.Headers.Append("x-total-count", cout.ToString());

        return Ok(response);
    }

    [HttpGet("{id:long}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<CarResponse>> GetCarById(
        long id, CancellationToken ct)
    {
        var dto = await carService.GetCarById(id, ct);
        var response = mapper.Map<CarResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult> CreateCar(
        [FromBody] CarRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<CarCreateModel>(request);
        var id = await carService.CreateCar(createModel, ct);

        var createdDto = await carService.GetCarById(id, ct);
        var response = mapper.Map<CarResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetCarById), 
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult> UpdateCar(
        long id, [FromBody]CarUpdateRequest request, CancellationToken ct)
    {
        var updateModel = mapper.Map<CarUpdateModel>(request);
        await carService.UpdateCar(id, updateModel, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult> DeleteCar(
        long id, CancellationToken ct)
    {
        await carService.DeleteCar(id, ct);

        return NoContent();
    }
}
