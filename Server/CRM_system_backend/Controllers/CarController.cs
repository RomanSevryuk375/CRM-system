using AutoMapper;
using CRM_system_backend.Contracts.Car;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Car;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IMapper _mapper;

    public CarController(
        ICarService carService,
        IMapper mapper)
    {
        _carService = carService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<CarItem>>> GetPagedCars([FromQuery]CarFilter filter)
    {
        var dto = await _carService.GetPagedCars(filter);
        var cout = await _carService.GetCountCars(filter);

        var response = _mapper.Map<List<CarResponse>>(dto);

        Response.Headers.Append("x-total-count", cout.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarItem>> GetCarById(long id)
    {
        var car = await _carService.GetCarById(id);

        return Ok(car);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateCar([FromBody] CarRequest request)
    {
        Console.WriteLine($"CONTROLLER DEBUG: Recieved JSON mapped to: OwnerId={request.OwnerId}, StatusId={request.StatusId}");
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

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _carService.CreateCar(car!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateCar(long id, [FromBody]CarUpdateRequest request)
    {
        var model = new CarUpdateModel(
            request.StatusId,
            request.Brand,
            request.Model,
            request.YearOfManufacture,
            request.Mileage);

        var Id = await _carService.UpdateCar(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteCar(long id)
    {
        var Id = await _carService.DeleteCar(id);

        return Ok(Id);
    }
}
