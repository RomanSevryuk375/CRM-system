using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
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

    [HttpGet("with owner")]

    public async Task<ActionResult<CarWithOwnerDto>> GetCarsWithOwner()
    {
        var dtos = await _carService.GetCarsWithOwner();

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

        return Ok(responses);
    }

    [HttpGet]

    public async Task<ActionResult<List<Car>>> GetCar()
    {
        var cars = await _carService.GetAllCars();

        var response = cars 
            .Select(b => new CarResponse(
                b.Id, b.OwnerId, b.Brand, b.Model, b.YearOfManufacture, b.VinNumber, b.StateNumber, b.Mileage))
            .ToList();

        return Ok(response);
    }

    [HttpPost]

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

        if(!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var carId = await _carService.CreateCar(car);
        return Ok(carId);
    }

    [HttpPut]

    public async Task<ActionResult<int>> UpdateCar([FromBody] CarUpdateRequest carUpdateRequest, int id)
    {
        var result = await _carService.UpdateCar(id,carUpdateRequest.Brand, carUpdateRequest.Model, carUpdateRequest.YearOfManufacture, carUpdateRequest.VinNumber, carUpdateRequest.StateNumber, carUpdateRequest.Mileage);
        
        return Ok(result);
    }

    [HttpDelete]

    public async Task<ActionResult<int>> DeleteCar(int id)
    {
        var result = await _carService.DeleteCar(id);

        return Ok(result);
    }
}
