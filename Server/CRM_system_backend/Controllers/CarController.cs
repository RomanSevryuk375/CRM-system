using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet("All-with-owner")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<CarWithOwnerDto>> GetCarsWithOwner(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var dtos = await _carService.GetCarsWithOwner(page, limit);
        var totalCount = await _carService.GetCountAllCars();

        var responses = dtos.Select(d => new CarWithOwnerDto(
            d.Id,
            d.OwnerId,
            d.OwnerFullName,
            d.Brand,
            d.Model,
            d.YearOfManufacture,
            d.VinNumber,
            d.StateNumber,
            d.Mileage
        )).ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(responses);
    }

    [HttpGet("All")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<Car>>> GetCar(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var cars = await _carService.GetAllCars(page, limit);
        var totalCount = await _carService.GetCountAllCars();

        var response = cars
            .Select(b => new CarResponse(
                b.Id, b.OwnerId, b.Brand, b.Model, b.YearOfManufacture, b.VinNumber, b.StateNumber, b.Mileage))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("My")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<List<Car>>> GetCarsByOwnerId(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var cars = await _carService.GetCarsByOwnerId(userId, page, limit);
        var totalCount = await _carService.GetCountCarsByOwnerId(userId);

        var response = cars
            .Select(b => new CarResponse(
                b.Id, b.OwnerId, b.Brand, b.Model, b.YearOfManufacture, b.VinNumber, b.StateNumber, b.Mileage))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("InWork")]
    [Authorize(Policy = "WorkerPolicy")]
    public async Task<ActionResult<List<Car>>> GetCarsByWorker(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var cars = await _carService.GetCarsForWorker(userId);

        var response = cars
            .Select(b => new CarResponse(
                b.Id, b.OwnerId, b.Brand, b.Model, b.YearOfManufacture, b.VinNumber, b.StateNumber, b.Mileage))
            .ToList();

        return Ok(response);
    }



    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<int>> CreateCar([FromBody] CarRequest carRequest)
    {
        var (car, error) = Car.Create(
            0,
            carRequest.OwnerId,
            carRequest.Brand,
            carRequest.Model,
            carRequest.YearOfManufacture,
            carRequest.VinNumber,
            carRequest.StateNumber,
            carRequest.Mileage);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var carId = await _carService.CreateCar(car);

        return Ok(carId);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "UserPolicy")]
    [Authorize(Policy = "WorkerPolicy")]
    public async Task<ActionResult<int>> UpdateCar([FromBody] CarUpdateRequest carUpdateRequest, int id)
    {
        var result = await _carService.UpdateCar(id, carUpdateRequest.Brand, carUpdateRequest.Model, carUpdateRequest.YearOfManufacture, carUpdateRequest.VinNumber, carUpdateRequest.StateNumber, carUpdateRequest.Mileage);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<int>> DeleteCar(int id)
    {
        var result = await _carService.DeleteCar(id);

        return Ok(result);
    }

}
