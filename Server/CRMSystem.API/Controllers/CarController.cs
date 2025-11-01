using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IClientService _clientService;

    public CarController(ICarService carService, IClientService clientService)
    {
        _carService = carService;
        _clientService = clientService;
    }

    [HttpGet]

    public async Task<ActionResult<List<Car>>> GetCar()
    {
        var cars = await _carService.GetCars();
        var clients = await _clientService.GetClients();

        var response = (from c in cars
                        join cl in clients on c.OwnerId equals cl.Id
                        select new CarResponse(
                            c.Id,
                            c.OwnerId,// а нужно ли мне эта хуета 
                            $"{cl.Name} {cl.Surname}",
                            c.Brand,
                            c.Model,
                            c.YearOfManufacture,
                            c.VinNumber,
                            c.StateNumber,
                            c.Mileage
                       )).ToList();

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
        var result = await _carService.UpdateCar(
            id,
            carUpdateRequest.Brand,
            carUpdateRequest.Model,
            carUpdateRequest.YearOfManufacture,
            carUpdateRequest.VinNumber,
            carUpdateRequest.StateNumber,
            carUpdateRequest.Mileage);
        
        return Ok(result);
    }

    [HttpDelete]

    public async Task<ActionResult<int>> DeleteCar(int id)
    {
        var result = await _carService.DeleteCar(id);

        return Ok(result);
    }
}
