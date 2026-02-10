using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Car;
using Shared.Enums;
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
    public async Task<ActionResult<List<CarItem>>> GetPagedCars(
        [FromQuery]CarFilter filter, CancellationToken ct)
    {
        var dto = await carService.GetPagedCars(filter, ct);
        var cout = await carService.GetCountCars(filter, ct);

        var response = mapper.Map<List<CarResponse>>(dto);

        Response.Headers.Append("x-total-count", cout.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<CarItem>> GetCarById(
        long id, CancellationToken ct)
    {
        var car = await carService.GetCarById(id, ct);

        return Ok(car);
    }

    [HttpPost]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult> CreateCar(
        [FromBody] CarRequest request, CancellationToken ct)
    {
        var (car, errors) = Car.Create(
            0,
            request.OwnerId,
            (CarStatusEnum)request.StatusId,
            request.Brand,
            request.Model,
            request.YearOfManufacture,
            request.VinNumber,
            request.StateNumber,
            request.Mileage);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await carService.CreateCar(car!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult> UpdateCar(
        long id, [FromBody]CarUpdateRequest request, CancellationToken ct)
    {
        var model = new CarUpdateModel(
            request.StatusId,
            request.Brand,
            request.Model,
            request.YearOfManufacture,
            request.Mileage);

        await carService.UpdateCar(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult> DeleteCar(
        long id, CancellationToken ct)
    {
        await carService.DeleteCar(id, ct);

        return NoContent();
    }
}
